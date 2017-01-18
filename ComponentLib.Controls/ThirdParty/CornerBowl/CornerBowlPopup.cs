/*
Copyright © 2010 Corner Bowl Software Corporation
All rights reserved.
http://www.CornerBowl.com
Licensed under Microsoft Public License (Ms-PL)
http://www.microsoft.com/opensource/licenses.mspx
*/

namespace ComponentLib.Controls.ThirdParty.CornerBowl
{
    #region Using directives
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    
    #endregion
    
    public class CornerBowlPopup : Component
    {
        #region Constants
        
        private const int DEFAULT_GOLDEN_RATIO_SAMPLE_RATE  = 3;
        private const int DEFAULT_LEFT_POPUP_MARGIN         = 1;
        private const int DEFAULT_MAX_POPUP_WIDTH           = 500;
        private const int DEFAULT_RIGHT_POPUP_MARGIN        = 4;
        private const int DEFAULT_SHADOW_DEPTH              = 5;
        private const int MIN_MAX_POPUP_WIDTH               = 100;
        private const int MAX_GOLDEN_RATIO_SAMPLE_RATE      = 10;
        private const string CATEGORY                       = "Appearance";
        private const string DEFAULT_F1HELP                 = "Press F1 for more help";
        private const string DEFAULT_MESSAGE                = "Provides a simple icon users can easily identify as a help provider. Once a user moves their mouse over the icon, a visually pleasing popup window displays context sensitive help that remains visible until they choose to close the view.";
        private const string DEFAULT_TITLE                  = "Corner Bowl Popup Help";
        
        #endregion
        
        #region Variables
        
        #region Fonts
        
        private Font f1HelpFont = new Font("Segoe UI", 8.25f, FontStyle.Bold);
        private Font messageFont = new Font("Segoe UI", 8.25f);
        private Font titleFont = new Font("Segoe UI", 9.75f, FontStyle.Bold);
        
        #endregion
        
        #region Maximum Width and Shadow
        
        private int goldenRatioSampleRate = DEFAULT_GOLDEN_RATIO_SAMPLE_RATE;
        private int maximumPopupWidth = DEFAULT_MAX_POPUP_WIDTH;
        private int shadowDepth = DEFAULT_SHADOW_DEPTH;
        
        #endregion
        
        #region Popup Margins
        
        private int leftPopupMargin = DEFAULT_LEFT_POPUP_MARGIN;
        private int rightPopupMargin = DEFAULT_RIGHT_POPUP_MARGIN;
        
        #endregion
        
        private CornerBowlPopupForm form;
        private Image helpImage;
        
        #endregion
        
        public CornerBowlPopup()
        {
            this.F1HelpText = DEFAULT_F1HELP;
            this.MessageText = DEFAULT_MESSAGE;
            this.TitleText = DEFAULT_TITLE;
            
            this.UseGoldenRatio = true;
            
            this.F1HelpPadding = new Padding(6, 4, 6, 4);
            this.MessagePadding = new Padding(12, 6, 12, 2);
            this.TitlePadding = new Padding(6, 8, 6, 0);
            
            this.F1HelpForeColor = Color.FromArgb(64, 64, 64);
            this.MessageForeColor = Color.FromArgb(64, 64, 64);
            this.TitleForeColor = Color.FromArgb(64, 64, 64);
            
            this.BorderColor = Color.FromArgb(118, 118, 118);
            this.DarkBackgroundColor = Color.FromArgb(201, 217, 239);
            this.DarkLineColor = Color.FromArgb(158, 187, 221);
            this.LightBackgroundColor = Color.White;
            this.LightLineColor = Color.White;
            
            this.helpImage = Resources.help;
        }
        
        #region Properties
        
        #region Text
        
        [Category(CATEGORY), DefaultValue(DEFAULT_F1HELP)]
        public string F1HelpText { get; set; }

        [DefaultValue(DEFAULT_MESSAGE), Browsable(true)]
        public string MessageText { get; set; }

        [Category(CATEGORY), DefaultValue(DEFAULT_TITLE)]
        public string TitleText { get; set; }

        #endregion

        #region Fonts
        
        [Category(CATEGORY), DefaultValue(typeof(Font), "Segoe UI, 8.25pt, style=Bold")]
        public Font F1HelpFont
        {
            get
            {
                return this.f1HelpFont;
            }
            
            set
            {
                if (this.f1HelpFont != null)
                {
                    this.f1HelpFont.Dispose();
                }
                
                this.f1HelpFont = value;
            }
        }

        [Category(CATEGORY), DefaultValue(typeof(Font), "Segoe UI, 8.25pt")]
        public Font MessageFont
        {
            get
            {
                return this.messageFont;
            }
            
            set
            {
                if (this.messageFont != null)
                {
                    this.messageFont.Dispose();
                }
                
                this.messageFont = value;
            }
        }

        [Category(CATEGORY), DefaultValue(typeof(Font), "Segoe UI, 9.75pt, style=Bold")]
        public Font TitleFont
        {
            get
            {
                return this.titleFont;
            }
            
            set
            {
                if (this.titleFont != null)
                {
                    this.titleFont.Dispose();
                }
                
                this.titleFont = value;
            }
        }
        
        #endregion

        #region Maximum Width and Shadow
        
        [Category(CATEGORY), DefaultValue(true)]
        public bool UseGoldenRatio { get; set; }

        [Category(CATEGORY), DefaultValue(DEFAULT_GOLDEN_RATIO_SAMPLE_RATE)]
        public int GoldenRatioSampleRate
        {
            get
            {
                return this.goldenRatioSampleRate;
            }
            
            set 
            {
                if (value <= 0)
                {
                    value = 1;
                }
                
                if (value > MAX_GOLDEN_RATIO_SAMPLE_RATE)
                {
                    value = MAX_GOLDEN_RATIO_SAMPLE_RATE;
                }
                
                this.goldenRatioSampleRate = value; 
            }
            
        }

        [Category(CATEGORY), DefaultValue(DEFAULT_MAX_POPUP_WIDTH)]
        public int MaximumPopupWidth
        {
            get
            {
                return this.maximumPopupWidth;
            }
            
            set
            {
                if (value < MIN_MAX_POPUP_WIDTH)
                {
                    value = MIN_MAX_POPUP_WIDTH;
                }
                
                this.maximumPopupWidth = value;
            }
        }

        [Category(CATEGORY), DefaultValue(DEFAULT_SHADOW_DEPTH)]
        public int ShadowDepth
        {
            get
            {
                return this.shadowDepth;
            }
            
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                
                this.shadowDepth = value;
            }
        }
        
        #endregion

        #region Popup Margins
        
        [Category(CATEGORY), DefaultValue(DEFAULT_LEFT_POPUP_MARGIN)]
        public int LeftPopupMargin
        {
            get
            {
                return this.leftPopupMargin;
            }
            
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                
                this.leftPopupMargin = value;
            }
        }

        [Category(CATEGORY), DefaultValue(DEFAULT_RIGHT_POPUP_MARGIN)]
        public int RightPopupMargin
        {
            get
            {
                return this.rightPopupMargin;
            }
            
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                
                this.rightPopupMargin = value;
            }
        }
        
        #endregion

        #region Padding
        
        [Category(CATEGORY), DefaultValue(typeof(Padding), "6, 4, 6, 4")]
        public Padding F1HelpPadding { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Padding), "12, 6, 12, 2")]
        public Padding MessagePadding { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Padding), "6, 8, 6, 0")]
        public Padding TitlePadding { get; set; }

        #endregion

        #region Fore Colors
        
        [Category(CATEGORY), DefaultValue(typeof(Color), "64, 64, 64")]
        public Color F1HelpForeColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "64, 64, 64")]
        public Color MessageForeColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "64, 64, 64")]
        public Color TitleForeColor { get; set; }
        
        #endregion

        #region Background Colors
        
        [Category(CATEGORY), DefaultValue(typeof(Color), "118, 118, 118")]
        public Color BorderColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "201, 217, 239")]
        public Color DarkBackgroundColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "158, 187, 221")]
        public Color DarkLineColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "255, 255, 255")]
        public Color LightBackgroundColor { get; set; }

        [Category(CATEGORY), DefaultValue(typeof(Color), "255, 255, 255")]
        public Color LightLineColor { get; set; }
        
        #endregion
        
        public Image HelpImage
        {
            get { return this.helpImage; }
        }
        
        #endregion
        
        #region Base Class Properties
        
        /*[Browsable(false)]
        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [DefaultValue(typeof(ImageLayout), "None"), Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        [Browsable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DefaultValue(typeof(Size), "16, 16"), Browsable(false)]
        public new Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        [DefaultValue(typeof(Size), "16, 16"),  Browsable(false)]
        public new Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [Browsable(false)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DefaultValue(0), Browsable(false)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [DefaultValue(false), Browsable(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = false; }
        }*/
        
        #endregion
        
        #region Functions
        
        public void ShowPopup(Point location)
        {
            if (this.form == null)
            {
                this.form = new CornerBowlPopupForm(this, location);
            }
            else
            {
                this.form.Location = location;
            }
        }

        public void HidePopup()
        {
            if (this.form != null)
            {
                this.form.Close();
                this.form.Dispose();
                this.form = null;
            }
        }
        
        #endregion
    }
}
