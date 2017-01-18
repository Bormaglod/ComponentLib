//-----------------------------------------------------------------------
// <copyright file="NetTabControl.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2015 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergio.teplyashin@gmail.com</email>
// <license>
//     This program is free software; you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation; either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <date>18.11.2006</date>
// <time>16:52</time>
// <summary>Defines the NetTabControl class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    
    public class NetTabControl : ContainerControl, ISupportInitialize
    {
        /// <summary>
        /// Высота закладки в пикселах.
        /// </summary>
        int tabHeight;
        
        /// <summary>
        /// Расположение закладок. Возможные значения:
        /// <para>Left - слева;</para>
        /// <para>Right - справа;</para>
        /// <para>Top - сверху;</para>
        /// <para>Bottom - снизу.</para>
        /// </summary>
        TabAlignment tabAlignment;
        
        /// <summary>
        /// Цвет границы страницы и панели закладок.
        /// </summary>
        Color borderColor;
        
        /// <summary>
        /// Карандаш для borderColor.
        /// </summary>
        Pen borderPen;
        
        /// <summary>
        /// Кисть для фона.
        /// </summary>
        SolidBrush backBrush;
        
        /// <summary>
        /// Кисть для цвета символов заголовка страницы.
        /// </summary>
        SolidBrush foreBrush;
        
        /// <summary>
        /// Текущая страница.
        /// </summary>
        int currentPage;
        
        /// <summary>
        /// Список страниц.
        /// </summary>
        NetTabPageCollection tabPages;
        
        /// <summary>
        /// Смещение выбранной закладки относительно верхней границы
        /// панели заголовков.
        /// </summary>
        int tabOffsetSel;
        
        /// <summary>
        /// Смещение закладки относительно верхней границы
        /// панели заголовков.
        /// </summary>
        int tabOffsetNormal;
        
        /// <summary>
        /// Массив кнопок управления, состоящий из четырех элементов.
        /// </summary>
        Dictionary<TabButtonStyle, NetTabButton> buttons;
        
        /// <summary>
        /// Варианты использования кнопок:
        /// <para>None - кнопки отсутствуют;</para>
        /// <para>NextPrev - кнопку для управления закладками;</para>
        /// <para>Context - кнопка вызова контекстного меню,
        /// содержащего список закладок;</para>
        /// <para>All - сочетание NextPrev и Context.</para>
        /// </summary>
        TabButtonStyles buttonStyles;
        
        /// <summary>
        /// Указывает на присутствие кнопки, предназначенной для
        /// закрытия окна.
        /// </summary>
        bool closeButton;
        
        /// <summary>
        /// Смещение закладок по горизонтали в случаях, когда
        /// пследние не умещаются в строке полностью.
        /// </summary>
        int offsetX;
        
        /// <summary>
        /// Ширина закладки в пикселах.
        /// </summary>
        int tabWidth;
        
        /// <summary>
        /// Способ представления закладок.
        /// </summary>
        TabControlView tabView;
        
        /// <summary>
        /// Графический путь для выделенной закладки.
        /// </summary>
        GraphicsPath tabPathSelected;
        
        /// <summary>
        /// Графический путь для невыделенной закладки.
        /// </summary>
        GraphicsPath tabPathNormal;
        
        /// <summary>
        /// Радиус углов закладки.
        /// </summary>
        int tabRadius;
        
        /// <summary>
        /// Номер подсвеченой закладки с помощью указателя мыши.
        /// </summary>
        int hoverPage;
        
        /// <summary>
        /// Указывает на необходимость выделять цветом закладку при наведении
        /// на нее указателя мыши.
        /// </summary>
        bool hoverEnable;
        
        /// <summary>
        /// Направление текста на закладке. Возможные значения:
        /// <para>Horizontal - горизонтально;</para>
        /// <para>Vertical - вертикально.</para>
        /// </summary>
        TabTextDirection textDirection;
        
        /*        
        /// <summary>
        /// Указывает, что процесс создания кнопок не завершен и
        /// размещение их функцией RelocationButtons невозможно.
        /// </summary>
        private bool initProcess = true;
        
        /// <summary>
        /// Номер страницы для которой выдается подсказка
        /// </summary>
        private int toolTipPage;
        
        // Подсказка для закладки
        private ToolTip toolTip;*/
            
        public NetTabControl()
        {
            BackColor = Color.FromArgb(255, 251, 250, 251);
            ForeColor = SystemColors.ControlText;
            Size = new Size(200, 100);
            
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            this.tabPages = new NetTabPageCollection(this);
            this.tabPages.ClearComplete += new EventHandler<EventArgs>(this.TabPagesClearComplete);
            this.tabPages.InsertComplete += new EventHandler<NetTabPageEventArgs>(this.TabPagesInsertComplete);
            this.tabPages.RemoveComplete += new EventHandler<NetTabPageEventArgs>(this.TabPagesRemoveComplete);

            this.TabHeight = 25;
            this.tabAlignment = TabAlignment.Top;
            this.currentPage = -1;
            
            this.tabOffsetSel = 3;
            this.tabOffsetNormal = 6;
            
            this.tabView = TabControlView.OneNote;
            this.buttonStyles = TabButtonStyles.All;
            this.closeButton = false;
            this.textDirection = TabTextDirection.Horizontal;
            
            this.offsetX = 0;
            this.tabWidth = 70;
            this.tabRadius = 10;
            
            this.tabPathSelected = new GraphicsPath();
            this.tabPathNormal = new GraphicsPath();
            
            this.hoverEnable = true;
            this.hoverPage = -1;
            
            /*this.toolTipPage = -1;
            this.toolTip = new ToolTip();
            this.toolTip.Active = true;*/
            
            this.CreateColors();
        }
        
        public event EventHandler<NetPageSelectedEventArgs> PageSelecting;
        public event EventHandler<NetPageSelectedEventArgs> PageSelected;
        
        /// <summary>
        /// Свойство возвращает или устанавливает цвет закладок.
        /// </summary>
        [Category("Appearance")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor TabColor { get; set; }
        
        /// <summary>
        /// Свойство возвращает или устанавливает цвет закладки при наведении на нее указателя мыши.
        /// </summary>
        [Category("Appearance")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor TabHover { get; set; }
        
        /// <summary>
        /// Свойство возвращает или устанавливает вариант расположения закладок. Возможные значения:
        /// <para>Left - слева;</para>
        /// <para>Right - справа;</para>
        /// <para>Top - сверху;</para>
        /// <para>Bottom - снизу.</para>
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(TabAlignment.Top)]
        public TabAlignment Alignment
        {
            get
            {
                return tabAlignment;
            }
            
            set
            {
                if (tabAlignment != value)
                {
                    tabAlignment = value;
                    CalculateAll();
                }
            }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает цвет границы страницы и панели закладок.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "135; 155; 179")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            
            set
            {
                if (borderColor != value)
                {
                    if (IsHandleCreated)
                    {
                        borderPen.Dispose();
                    }
                    
                    borderColor = value;
                    if (IsHandleCreated)
                    {
                        borderPen = new Pen(borderColor);
                    }
                    
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает текущую страницу.
        /// </summary>
        /// <seealso cref="NetTabPage"></seealso>
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                return currentPage;
            }
            
            set
            {
                SetCurentPage(value);
            }
        }
        
        /// <summary>
        /// Свойство возвращает список страниц <see cref="NetTabPage"></see>.
        /// </summary>
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NetTabPageCollection TabPages
        {
            get { return tabPages; }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает высоту закладок в пикселах.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(25)]
        public int TabHeight
        {
            get
            {
                return tabHeight;
            }
            
            set
            {
                if ((value > 16) && (tabHeight != value))
                {
                    tabHeight = value;
                    CalculateAll();
                }
            }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает варинты отображения кнопок управления отображаемых на <see cref="NetTabControl"></see>
        /// </summary>
        /// <remarks>
        /// Есть несколько вариантов отображения кнопок:
        /// <para>None - не отображается ни одной кнопки;</para>
        /// <para>NextPrev - отображаются только кнопки управления закладками;</para>
        /// <para>Context - отображается кнопка для вызова контекстного меню, содержащего весь список закладок;</para>
        /// <para>All - отображаются все кнопки.</para>
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(TabButtonStyles.All)]
        public TabButtonStyles ButtonStyles
        {
            get
            {
                return buttonStyles;
            }
            
            set
            {
                if (buttonStyles != value)
                {
                    buttonStyles = value;
                    CalculateLocationButtons();
                }
            }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает признак присутствия кнопки, предназначенной для
        /// закрытия страницы.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool CloseButton
        {
            get
            {
                return closeButton;
            }
            
            set
            {
                if (closeButton != value)
                {
                    closeButton = value;
                    CalculateLocationButtons();
                }
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(TabControlView.OneNote)]
        public TabControlView TabView
        {
            get
            {
                return tabView;
            }
            
            set
            {
                if (tabView != value)
                {
                    tabView = value;
                    tabOffsetSel = 3;
                    tabOffsetNormal = this.tabView == TabControlView.Buttons ? 3 : 6;
                    CalculateAll();
                    Invalidate();
                }
            }
        }
        
        /// <summary>
        /// Свойство возвращает или устанавливает направление текста на закладке. Возможные значения:
        /// <para>Horizontal - горизонтально;</para>
        /// <para>Vertical - вертикально.</para>
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(TabTextDirection.Horizontal)]
        public TabTextDirection TextDirection
        {
            get
            {
                return textDirection;
            }
            
            set
            {
                if (textDirection != value)
                {
                    textDirection = value;
                    CalculateAll();
                }
            }
        }
        
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool HoverEnable
        {
            get
            {
                return hoverEnable;
            }
            
            set
            {
                if (hoverEnable != value)
                {
                    hoverEnable = value;
                    Invalidate();
                }
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        new public Control.ControlCollection Controls
        {
            get { return base.Controls; }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        new public Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }
        
        new public Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            
            set
            {
                if (backBrush != null)
                {
                    backBrush.Dispose();
                }
                
                base.BackColor = value;
                backBrush = new SolidBrush(value);
                Invalidate();
            }
        }
        
        new public Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            
            set
            {
                if (foreBrush != null)
                {
                    foreBrush.Dispose();
                }
                
                base.ForeColor = value;
                foreBrush = new SolidBrush(value);
                Invalidate();
            }
        }
        
        /*[Category("TabControl common")]
        [DefaultValue(70)]
        public int TabWidth
        {
            get
            {
                return this.tabWidth;
            }
            
            set
            {
                if ((value > 25) && (this.tabWidth != value))
                {
                    this.tabWidth = value;
                    this.TabPages.UpdatePagesSettings();
                    this.GenerateTitlePath();
                    this.UpdateColors();
                    Invalidate();
                    if (this.currentPage >= 0)
                    {
                        this.TabPages[this.currentPage].Visible = true;
                    }
                }
            }
        }
        
        
        
        [Category("TabControl common")]
        [DefaultValue(10)]
        public int TabRadius
        {
            get
            {
                return this.tabRadius;
            }
            
            set
            {
                if ((value <= this.tabHeight) && (value > 0) && (this.tabRadius != value))
                {
                    this.tabRadius = value;
                    this.TabPages.UpdatePagesSettings();
                    this.GenerateTitlePath();
                    this.UpdateColors();
                    Invalidate();
                    if (this.currentPage >= 0)
                    {
                        this.TabPages[this.currentPage].Visible = true;
                    }
                }
            }
        }
        
        [Category("Tabs property")]
        [DefaultValue(6)]
        public int TabOffset
        {
            get
            {
                return this.tabOffsetNormal;
            }
            
            set
            {
                if ((this.tabOffsetNormal != value) && (this.tabOffsetNormal > this.tabOffsetSel) && (this.tabOffsetNormal < this.tabHeight - 2))
                {
                    this.tabOffsetNormal = value;
                    this.UpdateColors();
                    Invalidate();
                }
            }
        }
        
        
        
        [Category("TabControl common")]
        [DefaultValue(true)]
        public bool EnableToolTip
        {
            get
            {
                return this.toolTip.Active;
            }
            
            set
            {
                this.toolTip.RemoveAll();
                this.toolTip.Active = value;
            }
        }*/
        
        internal Rectangle BoundsNormalTab
        {
            get { return GetBoundsHeader(tabOffsetNormal); }
        }
        
        internal Rectangle BoundsSelectTab
        {
            get { return GetBoundsHeader(tabOffsetSel); }
        }
        
        Rectangle DefaultBoundsHeader
        {
            get { return GetBoundsHeader(); }
        }
        
        Rectangle BoundButtons
        {
            get { return GetBoundsButtons(); }
        }
        
        /// <summary>
        /// Свойство возвращает количества видимых кнопок.
        /// </summary>
        int CountButtons
        {
            get
            {
                int res = 0;
                foreach (Control c in Controls)
                {
                    NetTabButton b = c as NetTabButton;
                    if (b != null && b.Visible)
                    {
                        res++;
                    }
                }
                
                return res;
            }
        }
        
        #region ISupportInitialize methods
        
        public void BeginInit()
        {
        }
        
        public void EndInit()
        {
            // Создадим кнопки
            buttons = new Dictionary<TabButtonStyle, NetTabButton>();
            buttons.Add(TabButtonStyle.LeftDown, new NetTabButton(this, TabButtonAction.Left));
            buttons.Add(TabButtonStyle.RightUp, new NetTabButton(this, TabButtonAction.Right));
            buttons.Add(TabButtonStyle.Action, new NetTabButton(this, TabButtonAction.Action));
            buttons.Add(TabButtonStyle.Close, new NetTabButton(this, TabButtonAction.Close));
            
            // Разместим кнопки на форме...
            CalculateLocationButtons();
            
            // и добавим их на нее
            for (int i = 0; i < 4; i++)
            {
                NetTabButton b = buttons[(TabButtonStyle)i];
                Controls.Add(b);
                b.ButtonPresed += new EventHandler<EventArgs>(ActionButtonPressed);
            }
            
            GenerateTitlePath();
        }
        
        #endregion
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            g.DrawRectangle(borderPen, DefaultBoundsHeader);
            
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            if (TabPages.Count > 0 && currentPage != -1)
            {
                for (int i = TabPages.Count - 1; i >= 0; i--)
                {
                    if (currentPage != i)
                    {
                        DrawTab(g, i);
                    }
                }
                
                if (currentPage >= 0 && currentPage < TabPages.Count)
                {
                    DrawTab(g, currentPage);
                }
                
                DrawButtons(g);
            }
            
            g.SmoothingMode = SmoothingMode.None;
            g.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (BoundButtons.Contains(e.X, e.Y))
            {
                bool needRedraw = hoverPage != -1;
                hoverPage = -1;
                if (needRedraw)
                {
                    Invalidate();
                }
            }
            else
            {
                int newHoverPage = GetHitTest(e.X, e.Y);
                bool needRedraw = false;
                
                if (HoverEnable)
                {
                    if (newHoverPage != hoverPage)
                    {
                        hoverPage = newHoverPage == currentPage ? -1 : newHoverPage;
                        needRedraw = true;
                    }
                }
                
                if (needRedraw)
                {
                    Invalidate();
                }

                /*if( toolTip.Active )
                {
                    if( toolTipPage != newHoverPage )
                    {
                        toolTipPage = newHoverPage;

                        toolTip.RemoveAll();
                        if( newHoverPage != -1 )
                            toolTip.SetToolTip(this, TabPages[newHoverPage].ToolTip);
                    }
                }*/
            }
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!BoundButtons.Contains(e.X, e.Y))
            {
                int newPage = GetHitTest(e.X, e.Y);

                if (newPage >= 0 && newPage != currentPage)
                {
                    SelectedIndex = newPage;
                    hoverPage = -1;
                    Invalidate();
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                borderPen.Dispose();
                backBrush.Dispose();
            }
            
            base.Dispose(disposing);
        }
        
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            NetTabPage page = e.Control as NetTabPage;
            if (page != null && string.IsNullOrEmpty(page.Title))
            {
                page.Title = page.Name;
            }
        }
        
        protected bool OnCanPageSelect(NetTabPage page)
        {
            if (PageSelecting != null)
            {
                NetPageSelectedEventArgs e = new NetPageSelectedEventArgs(page);
                PageSelecting(this, e);
                return e.CanSelect;
            }
            
            return true;
        }
        
        protected void OnPageSelected(NetTabPage page)
        {
            if (PageSelected != null)
            {
                PageSelected(this, new NetPageSelectedEventArgs(page));
            }
        }
        
        void CreateColors()
        {
            TabColor = new GradientColor(Color.White, Color.Orange, GradientFill.Top);
            TabHover = new GradientColor(Color.Orange, Color.White, GradientFill.Top);
            
            borderColor = Color.FromArgb(255, 135, 155, 179);
            borderPen = new Pen(borderColor);
            backBrush = new SolidBrush(BackColor);
            foreBrush = new SolidBrush(ForeColor);
        }
        
        Rectangle GetBoundsHeader(int offset = 0)
        {
            int x = 0;
            int y = 0;
            int w = Width - 1;
            int h = Height - 1;
            switch (tabAlignment)
            {
                case TabAlignment.Top:
                    y = offset;
                    h = this.tabHeight - offset - 1;
                    break;
                case TabAlignment.Bottom:
                    y = Height - tabHeight;
                    h = tabHeight - offset - 1;
                    break;
                case TabAlignment.Left:
                    x = offset;
                    w = tabHeight - offset - 1;
                    break;
                case TabAlignment.Right:
                    x = Width - tabHeight;
                    w = tabHeight - offset - 1;
                    break;
            }
            
            return new Rectangle(x, y, w, h);
        }
        
        Rectangle GetBoundsButtons()
        {
            int x = 0, y = 0, h = 0, w = 0;
            int l = CountButtons * NetTabButton.SizeButton + 6;
            
            switch (tabAlignment)
            {
                case TabAlignment.Top:
                    x = Width - l;
                    y = 0;
                    w = l;
                    h = tabHeight;
                    break;
                case TabAlignment.Right:
                    x = Width - tabHeight - 1;
                    y = Height - l;
                    w = tabHeight;
                    h = l;
                    break;
                case TabAlignment.Left:
                    x = 0;
                    y = Height - l;
                    w = tabHeight;
                    h = l;
                    break;
                case TabAlignment.Bottom:
                    x = Width - l;
                    y = Height - tabHeight - 1;
                    w = l;
                    h = tabHeight;
                    break;
            }
            
            return new Rectangle(x, y, w, h);
        }
        
        Rectangle GetBoundsTab(int tab)
        {
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;
            int d = tabWidth;
            if (tabView == TabControlView.OneNote)
            {
                d += tabHeight >> 1;
            }
            
            int dh = (tab == currentPage)? tabOffsetSel : tabOffsetNormal;
            
            switch (tabAlignment)
            {
                case TabAlignment.Top:
                    x1 = tab * d - offsetX;
                    x2 = (tab + 1) * d - offsetX;
                    y1 = dh;
                    y2 = tabHeight;
                    break;
                case TabAlignment.Right:
                    x1 = Width - tabHeight - 1;
                    x2 = Width - 1 - dh;
                    y1 = tab * d - offsetX;
                    y2 = (tab + 1) * d - offsetX;
                    break;
                case TabAlignment.Left:
                    x1 = dh;
                    x2 = tabHeight;
                    y1 = tab * d - offsetX;
                    y2 = (tab + 1) * d - offsetX;
                    break;
                case TabAlignment.Bottom:
                    x1 = tab * d - offsetX;
                    x2 = (tab + 1) * d - offsetX;
                    y1 = Height - tabHeight - 1;
                    y2 = Height - 1 - dh;
                    break;
            }
            
            return new Rectangle(x1, y1, x2 - x1 - 1, y2 - y1 - 1);
        }
        
        /// <summary>
        /// Устанавливает текущей новую страницу.
        /// </summary>
        /// <param name="newPage">Индекс новой страницы.</param>
        void SetCurentPage(int newPage)
        {
            if (newPage != currentPage && newPage >= 0 && newPage < TabPages.Count)
            {
                if (OnCanPageSelect(TabPages[newPage]))
                {
                    TabPages[newPage].UpdateBackground();
                    TabPages[newPage].Visible = true;
                    if (currentPage >= 0 && currentPage < TabPages.Count)
                    {
                        TabPages[currentPage].Visible = false;
                    }
                    
                    if (currentPage != -1)
                    {
                        TabPages[currentPage].UpdateBackground();
                    }
                    
                    currentPage = newPage;
                    OnPageSelected(TabPages[currentPage]);
                    Invalidate();
                }
            }
        }
        
        void TabPagesClearComplete(object sender, EventArgs e)
        {
            Controls.Clear();
            currentPage = -1;
        }
        
        void TabPagesInsertComplete(object sender, NetTabPageEventArgs e)
        {
            Controls.Add(e.Page);
            if (SelectedIndex == -1)
            {
                SelectedIndex = 0;
            }
            
            e.Page.UpdateBackground();
        }
        
        void TabPagesRemoveComplete(object sender, NetTabPageEventArgs e)
        {
            Controls.Remove(e.Page);
            if (SelectedIndex >= TabPages.Count)
            {
                if (TabPages.Count == 0)
                {
                    currentPage = -1;
                }
                else
                {
                    SelectedIndex = TabPages.Count - 1;
                    TabPages[SelectedIndex].UpdateBackground();
                }
            }
        }
        
        /// <summary>
        /// В зависимости от свойства Alignment кнопки размещаются в
        /// соответствующем месте NetTabControl
        /// Количество и тип кнопок определяет свойство PageManager
        /// </summary>
        void CalculateLocationButtons()
        {
            if (buttons == null)
            {
                return;
            }
            
            foreach (TabButtonStyle style in buttons.Keys)
            {
                switch (buttons[style].ButtonAction)
                {
                    case TabButtonAction.None:
                        buttons[style].Visible = true;
                        break;
                    case TabButtonAction.Action:
                        buttons[style].Visible = (buttonStyles == TabButtonStyles.All) || (buttonStyles == TabButtonStyles.Context);
                        break;
                    case TabButtonAction.Close:
                        buttons[style].Visible = closeButton;
                        break;
                    case TabButtonAction.Down:
                    case TabButtonAction.Right:
                    case TabButtonAction.Up:
                    case TabButtonAction.Left:
                        buttons[style].Visible = (buttonStyles == TabButtonStyles.All) || (buttonStyles == TabButtonStyles.NextPrev);
                        break;
                }
            }
            
            int x = 0;
            int y = 0;
            int d = (tabHeight - NetTabButton.SizeButton + 1) >> 1;
            int dx = 0;
            int dy = 0;
            AnchorStyles styles = AnchorStyles.None;
            switch (this.Alignment)
            {
                case TabAlignment.Top:
                    y = d;
                    dx = -NetTabButton.SizeButton;
                    x = Width - 3 + dx;
                    buttons[TabButtonStyle.LeftDown].ButtonAction = TabButtonAction.Left;
                    buttons[TabButtonStyle.RightUp].ButtonAction = TabButtonAction.Right;
                    styles = AnchorStyles.Right | AnchorStyles.Top;
                    break;
                case TabAlignment.Bottom:
                    y = Height - this.tabHeight - 1 + d;
                    dx = -NetTabButton.SizeButton;
                    x = Width - 3 + dx;
                    buttons[TabButtonStyle.LeftDown].ButtonAction = TabButtonAction.Left;
                    buttons[TabButtonStyle.RightUp].ButtonAction = TabButtonAction.Right;
                    styles = AnchorStyles.Right | AnchorStyles.Bottom;
                    break;
                case TabAlignment.Left:
                    x = d;
                    dy = -NetTabButton.SizeButton;
                    y = Height - 3 + dy;
                    buttons[TabButtonStyle.LeftDown].ButtonAction = TabButtonAction.Up;
                    buttons[TabButtonStyle.RightUp].ButtonAction = TabButtonAction.Down;
                    styles = AnchorStyles.Left | AnchorStyles.Bottom;
                    break;
                case TabAlignment.Right:
                    x = Width - this.tabHeight - 1 + d;
                    dy = -NetTabButton.SizeButton;
                    y = Height - 3 + dy;
                    buttons[TabButtonStyle.LeftDown].ButtonAction = TabButtonAction.Up;
                    buttons[TabButtonStyle.RightUp].ButtonAction = TabButtonAction.Down;
                    styles = AnchorStyles.Right | AnchorStyles.Bottom;
                    break;
            }
            
            for (int i = 3; i >= 0; i--)
            {
                TabButtonStyle style = (TabButtonStyle)i;
                if (buttons[style].Visible)
                {
                    buttons[style].Location = new Point(x, y);
                    buttons[style].Anchor = styles;
                    x += dx;
                    y += dy;
                }
            }
        }
        
        void ActionButtonPressed(object sender, EventArgs e)
        {
            NetTabButton button = sender as NetTabButton;
            if ((button.ButtonAction == TabButtonAction.Left) || (button.ButtonAction == TabButtonAction.Up))
            {
                offsetX -= tabWidth;
                if (offsetX < 0)
                {
                    offsetX = 0;
                }
            }
            else if ((button.ButtonAction == TabButtonAction.Right) || (button.ButtonAction == TabButtonAction.Down))
            {
                int l = tabWidth;
                if (tabView == TabControlView.OneNote)
                {
                    l += tabHeight / 2 + 3;
                }
                
                offsetX += l;
                int full_l = l * TabPages.Count;
                int d;
                if (Alignment == TabAlignment.Top || Alignment == TabAlignment.Bottom)
                {
                    d = DefaultBoundsHeader.Width - BoundButtons.Width;
                }
                else
                {
                    d = DefaultBoundsHeader.Height - BoundButtons.Height;
                }
                
                if (full_l < d)
                {
                    offsetX = 0;
                }
                else
                {
                    if (d + offsetX > full_l)
                    {
                        offsetX = full_l - d;
                    }
                }
            }
            else if (button.ButtonAction == TabButtonAction.Action)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                foreach (NetTabPage page in TabPages)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = page.Title;
                    int n = TabPages.IndexOf(page);
                    item.Tag = n;
                    item.Checked = (n == currentPage);
                    item.Click += (senderItem, pe) => SelectedIndex = Convert.ToInt32((senderItem as ToolStripMenuItem).Tag);
                    
                    menu.Items.Add(item);
                }
                
                menu.Show(button, 0, button.Size.Height);
            }
            
            Invalidate();
        }
        
        void CalculateAll()
        {
            TabPages.UpdatePagesSettings(false);
            CalculateLocationButtons();
            GenerateTitlePath();
            Invalidate();
        }
        
        void GenerateTitlePathOffset(GraphicsPath path, int offset)
        {
            if (path == null)
            {
                return;
            }
            path.Reset();
            
            path.StartFigure();
            if (tabAlignment == TabAlignment.Left || tabAlignment == TabAlignment.Bottom)
            {
                switch (tabView)
                {
                    case TabControlView.Square:
                        path.AddLine(tabHeight - 1, 0, offset, 0);
                        path.AddLine(offset, 0, offset, tabWidth - 1);
                        path.AddLine(offset, tabWidth - 1, tabHeight - 1, tabWidth - 1);
                        break;
                    case TabControlView.OneNote:
                        path.AddLine(tabHeight - 1, 0, tabRadius + offset, 0);
                        path.AddArc(offset, 0, tabRadius, tabRadius, -90, -90);
                        path.AddLine(offset, tabRadius, offset, tabWidth - 1);
                        path.AddLine(offset, tabWidth - 1, tabHeight - 1, tabWidth + tabHeight);
                        break;
                    case TabControlView.Rounded:
                        path.AddLine(tabHeight - 1, 0, tabRadius + offset, 0);
                        path.AddArc(offset, 0, tabRadius, tabRadius, -90, -90);
                        path.AddLine(offset, tabRadius, offset, tabWidth - tabRadius);
                        path.AddArc(offset, tabWidth - tabRadius, tabRadius, tabRadius, -180, -90);
                        path.AddLine(tabRadius + offset, tabWidth, tabHeight - 1, tabWidth);
                        break;
                    case TabControlView.Buttons:
                        path.AddLine(tabHeight - 4, 3, offset, 3);
                        path.AddLine(offset, 3, offset, tabWidth - 1);
                        path.AddLine(offset, tabWidth, tabHeight - 4, tabWidth);
                        break;
                }
            }
            else
            {
                switch (tabView)
                {
                    case TabControlView.Square:
                        path.AddLine(0, tabHeight - 1, 0, offset);
                        path.AddLine(0, offset, tabWidth - 1, offset);
                        path.AddLine(tabWidth - 1, offset, tabWidth - 1, tabHeight - 1);
                        break;
                    case TabControlView.OneNote:
                        path.AddLine(0, tabHeight - 1, 0, tabRadius + offset);
                        path.AddArc(0, offset, tabRadius, tabRadius, 180, 90);
                        path.AddLine(tabRadius, offset, tabWidth - 1, offset);
                        path.AddLine(tabWidth - 1, offset, tabWidth + tabHeight, tabHeight - 1);
                        break;
                    case TabControlView.Rounded:
                        path.AddLine(0, tabHeight - 1, 0, tabRadius + offset);
                        path.AddArc(0, offset, tabRadius, tabRadius, 180, 90);
                        path.AddLine(tabRadius, offset, tabWidth - tabRadius, offset);
                        path.AddArc(tabWidth - tabRadius, offset, tabRadius, tabRadius, -90, 90);
                        path.AddLine(tabWidth, tabRadius + offset, tabWidth, tabHeight - 1);
                        break;
                    case TabControlView.Buttons:
                        path.AddLine(3, tabHeight - 4, 3, offset);
                        path.AddLine(3, offset, tabWidth, offset);
                        path.AddLine(tabWidth, offset, tabWidth, tabHeight - 4);
                        break;
                }
            }
            
            path.CloseFigure();
        }
        
        /// <summary>
        /// Создает два пути Graphics.Path для выбранной закладки и не выбранной
        /// </summary>
        void GenerateTitlePath()
        {
            GenerateTitlePathOffset(tabPathSelected, tabOffsetSel);
            GenerateTitlePathOffset(tabPathNormal, tabOffsetNormal);
        }
        
        GraphicsPath GetTitlePath(int nPage)
        {
            GraphicsPath path = new GraphicsPath();

            path.Reset();
            if (nPage == SelectedIndex)
            {
                path.AddPath(tabPathSelected, true);
            }
            else
            {
                path.AddPath(tabPathNormal, true);
            }

            Matrix m = new Matrix();
            
            int d = tabWidth;
            if (tabView == TabControlView.OneNote)
            {
                d += tabHeight / 2;
            }
            
            switch (tabAlignment)
            {
                case TabAlignment.Top:
                    m.Translate(d * nPage - offsetX, 0);
                    break;
                case TabAlignment.Right:
                    m.Rotate(90);
                    m.Translate(d * nPage - offsetX, -Width + 1);
                    break;
                case TabAlignment.Left:
                    m.Translate(0, nPage * d - offsetX);
                    break;
                case TabAlignment.Bottom:
                    m.Rotate(-90);
                    m.Translate(-Height + 1, nPage * d - offsetX);
                    break;
            }
            
            path.Transform(m);

            return path;
        }
        
        void DrawTab(Graphics g, int nPage)
        {
            GraphicsPath path = GetTitlePath(nPage);
            Rectangle rect = GetBoundsTab(nPage);
            
            Brush brush;
            
            if (hoverPage == nPage && hoverEnable)
            {
                brush = TabHover.Brush;
            }
            else if (currentPage == nPage)
            {
                brush = TabPages[nPage].TabActiveColor.Brush;
            }
            else
            {
                brush = TabPages[nPage].TabColor.Brush;
            }
            
            g.FillPath(brush, path);
            g.DrawPath(borderPen, path);
            
            if (currentPage == nPage && tabView != TabControlView.Buttons)
            {
                PointF p1 = new PointF(path.PathPoints[0].X, path.PathPoints[0].Y);
                PointF p2 = new PointF(path.PathPoints[path.PointCount - 1].X, path.PathPoints[path.PointCount - 1].Y);
                if (Alignment == TabAlignment.Bottom || Alignment == TabAlignment.Top)
                {
                    p1.X++;
                    p2.X--;
                }
                else
                {
                    p1.Y++;
                    p2.Y--;
                }
                
                g.SmoothingMode = SmoothingMode.None;
                g.DrawLine(tabPages[currentPage].PageColor.BorderPen, p1, p2);
            }
            
            StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;
            if (textDirection == TabTextDirection.Vertical)
            {
                f.FormatFlags = StringFormatFlags.DirectionVertical;
            }
            
            Font font;
            font = currentPage == nPage ? new Font(Font, FontStyle.Bold) : Font.Clone() as Font;
            
            Rectangle textRect = new Rectangle(rect.Location, rect.Size);
            if (TabView == TabControlView.OneNote)
            {
                if (tabAlignment == TabAlignment.Bottom || tabAlignment == TabAlignment.Top)
                {
                    textRect.Width -= tabHeight / 2;
                }
                else
                {
                    textRect.Height -= tabHeight / 2;
                }
            }
            
            g.DrawString(TabPages[nPage].Title, font, foreBrush, textRect, f);
            //            if (this.Focused)
            //            {
            //                textRect.X += 3; textRect.Y += 3;
            //                textRect.Width -= 7; textRect.Height -= 7;
            //                Pen p = new Pen(Color.Black);
            //                p.DashStyle = DashStyle.Dot;
            //                SmoothingMode sm = g.SmoothingMode;
            //                try
            //                {
            //                    g.SmoothingMode = SmoothingMode.None;
            //                    g.DrawRectangle(p, textRect);
            //                }
            //                finally
            //                {
            //                    g.SmoothingMode = sm;
            //                }
            //            }
        }
        
        void DrawButtons(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.None;
            Rectangle r = BoundButtons;
            r.Inflate(-1, -1);
            g.FillRectangle(backBrush, r);
            switch (Alignment)
            {
                case TabAlignment.Top:
                    g.DrawLine(borderPen, r.Left, r.Bottom, r.Right, r.Bottom);
                    break;
                case TabAlignment.Bottom:
                    g.DrawLine(borderPen, r.Left, r.Top, r.Right, r.Top);
                    break;
                case TabAlignment.Left:
                    g.DrawLine(borderPen, r.Right, r.Top, r.Right, r.Bottom);
                    break;
                case TabAlignment.Right:
                    g.DrawLine(borderPen, r.Left, r.Top, r.Left, r.Bottom);
                    break;
            }
        }
        
        /*
        private void UpdateColors()
        {
            if (this.tabColor != null)
            {
                this.tabColor.ColorChanged -= new EventHandler<EventArgs>(this.TabControlColorChanged);
            }
            
            this.tabColor.Update(this.GetBoundsNormalTab(), this.Alignment);
            this.tabColor.ColorChanged -= new EventHandler<EventArgs>(TabControlColorChanged);
            
            if (this.tabHover != null)
            {
                this.tabHover.ColorChanged -= new EventHandler<EventArgs>(this.TabControlColorChanged);
            }
            this.tabHover.Update(this.GetBoundsNormalTab(), this.Alignment);
            this.tabHover.ColorChanged += new EventHandler<EventArgs>(this.TabControlColorChanged);
        }
        
        private void TabControlColorChanged(object sender, EventArgs e)
        {
            Invalidate();
        }*/
        
        /*private Rectangle GetBoundsPage()
        {
            int x = 0, y = 0;
            int w = Width - 1, h = Height - 1;
            switch (this.tabAlignment)
            {
                case TabAlignment.Top:
                    y = this.tabHeight - 1;
                    h -= this.tabHeight - 1;
                    break;
                case TabAlignment.Bottom:
                    h -= this.tabHeight - 1;
                    break;
                case TabAlignment.Left:
                    x = this.tabHeight - 1;
                    w -= this.tabHeight - 1;
                    break;
                case TabAlignment.Right:
                    w -= this.tabHeight - 1;
                    break;
            }
            
            return new Rectangle(x, y, w, h);
        }
         */
        
        int GetHitTest(int X, int Y)
        {
            if (GetTitlePath(currentPage).IsVisible(X, Y) )
            {
                return currentPage;
            }

            for (int i = 0; i < TabPages.Count; i++)
            {
                if (currentPage != i)
                {
                    if (GetTitlePath(i).IsVisible(X, Y))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        
        
    }
}
