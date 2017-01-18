//-----------------------------------------------------------------------
// <copyright file="UnicodeCollection.cs" company="Sergey Teplyashin">
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
// <date>26.10.2011</date>
// <time>8:30</time>
// <summary>Defines the UnicodeCollection class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnicodeCollection
    {
        readonly List<UnicodeType> types;
        readonly List<UnicodeCategory> categories;
        
        public UnicodeCollection()
        {
            types = new List<UnicodeType>();
            categories = new List<UnicodeCategory>();
            CreateList();
        }
        
        public int CountTypes
        {
            get { return types.Count; }
        }
        
        public UnicodeType this[int index]
        {
            get { return types[index]; }
        }
        
        public UnicodeType this[string unicodeName]
        {
        	get { return types.FirstOrDefault(x => x.Name == unicodeName); }
        }
        
        public UnicodeType this[UnicodeIndex index]
        {
            get { return types.FirstOrDefault(x => x.Id == index); }
        }
        
        public IEnumerable<UnicodeCategory> GetCategories(UnicodeType unicodeType)
        {
            if (unicodeType == null)
            {
            	throw new ArgumentNullException("unicodeType");
            }
            
            return categories.Where(x => x.UnicodeType.Id == unicodeType.Id);
        }
        
        public IEnumerable<UnicodeType> GetTypes()
        {
            return types;
        }
        
        public UnicodeCategory GetCategory(UnicodeType unicodeType)
        {
        	return categories.FirstOrDefault(x => x.UnicodeType.Id == unicodeType.Id);
        }
        
        public UnicodeCategory GetCategory(string categoryName)
        {
        	return categories.FirstOrDefault(x => x.Name == categoryName);
        }
        
        void CreateList()
        {
            types.AddRange(new [] {
                               new UnicodeType(UnicodeIndex.Europe, Strings.European),
                               new UnicodeType(UnicodeIndex.Africa, Strings.African),
                               new UnicodeType(UnicodeIndex.NearEast, Strings.MiddleEast),
                               new UnicodeType(UnicodeIndex.SouthAsia, Strings.SouthAsian),
                               new UnicodeType(UnicodeIndex.Philippines, Strings.Philippines),
                               new UnicodeType(UnicodeIndex.SouthEastAsia, Strings.SouthEastAsian),
                               new UnicodeType(UnicodeIndex.EastAsia, Strings.EastAsian),
                               new UnicodeType(UnicodeIndex.CenterAsia, Strings.CentralAsian),
                               new UnicodeType(UnicodeIndex.OtherLanguages, Strings.OtherLanguages),
                               new UnicodeType(UnicodeIndex.Symbols, Strings.Symbols),
                               new UnicodeType(UnicodeIndex.Mathematics, Strings.Mathematics),
                               new UnicodeType(UnicodeIndex.Phonetic, Strings.Phonetic),
                               new UnicodeType(UnicodeIndex.Diacritic, Strings.Diacritic),
                               new UnicodeType(UnicodeIndex.Other, Strings.Other)
                           });
            
            
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.BasicLatin, 0x0000, 0x007F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.Latin1Supplement, 0x0080, 0x00FF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.LatinExtendedA, 0x0100, 0x017F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.LatinExtendedB, 0x0180, 0x024F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.LatinExtendedAdditional, 0x1E00, 0x1EFF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.LatinExtendedC, 0x2C60, 0x2C7F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.LatinExtendedD, 0xA720, 0xA7FF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.SmallFormVariants, 0xFE50, 0xFE6F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.Armenian, 0x0530, 0x058F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.Coptic, 0x2C80, 0x2CFF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.Cyrillic, 0x0400, 0x04FF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.CyrillicSupplement, 0x0500, 0x052F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.CyrillicExtendedA, 0x2DE0, 0x2DFF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.CyrillicExtendedB, 0xA640, 0xA6DF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.Georgian, 0x10A0, 0x10FF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.GeorgianSupplement, 0x2D00, 0x2D2F),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.GreekAndCoptic, 0x0370, 0x03FF),
                              new UnicodeCategory(this[UnicodeIndex.Europe], Strings.GreekExtended, 0x1F00, 0x1FFF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.Ethiopic, 0x1200, 0x137F),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.EthiopicSupplement, 0x1380, 0x139F),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.EthiopicExtended, 0x2D80, 0x2DDF),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.NKo, 0x07C0, 0x07FF),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.Tifinagh, 0x2D30, 0x2D7F),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.Vai, 0xA500, 0xA63F),
                              new UnicodeCategory(this[UnicodeIndex.Africa], Strings.Bamum, 0xA6A0, 0xA6FF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.Arabic, 0x0600, 0x06FF),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.ArabicSupplement, 0x0750, 0x077F),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.ArabicPresentationFormsA, 0xFB50, 0xFDFF),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.ArabicPresentationFormsB, 0xFE70, 0xFEFF),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.Hebrew, 0x0590, 0x05FF),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.Syriac, 0x0700, 0x074F),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.Thaana, 0x0780, 0x07BF),
                              new UnicodeCategory(this[UnicodeIndex.NearEast], Strings.Samaritan, 0x0800, 0x083F)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Bengali, 0x0980, 0x09FF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Devanagari, 0x0900, 0x097F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Gujarati, 0x0A80, 0x0AFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Gurmukhi, 0x0A00, 0x0A7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Kannada, 0x0C80, 0x0CFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Limbu, 0x1900, 0x194F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Malayalam, 0x0D00, 0x0D7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Oriya, 0x0B00, 0x0B7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Sinhala, 0x0D80, 0x0DFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.SylotiNagri, 0xA800, 0xA82F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Tamil, 0x0B80, 0x0BFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Telugu, 0x0C00, 0x0C7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Lepcha, 0x1C00, 0x1C4F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.OlChiki, 0x1C50, 0x1C7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.VedicExtensions, 0x1CD0, 0x1CFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.CommonIndicNumberForms, 0xA830, 0xA83F),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.Saurashtra, 0xA880, 0xA8DF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.DevanagariExtended, 0xA8E0, 0xA8FF),
                              new UnicodeCategory(this[UnicodeIndex.SouthAsia], Strings.MeeteiMayek, 0xABC0, 0xABFF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Philippines], Strings.Buhid, 0x1740, 0x175F),
                              new UnicodeCategory(this[UnicodeIndex.Philippines], Strings.Tagalog, 0x1700, 0x171F),
                              new UnicodeCategory(this[UnicodeIndex.Philippines], Strings.Hanunoo, 0x1720, 0x173F),
                              new UnicodeCategory(this[UnicodeIndex.Philippines], Strings.Tagbanwa, 0x1760, 0x177F)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Buginese, 0x1A00, 0x1A7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Balinese, 0x1B00, 0x1B7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Khmer, 0x1780, 0x17FF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.KhmerSymbols, 0x19E0, 0x19FF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Lao, 0x0E80, 0x0EFF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Myanmar, 0x1000, 0x109F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.MyanmarExtendedA, 0xAA60, 0xAA7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.TaiLe, 0x1950, 0x197F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.NewTaiLue, 0x1980, 0x19DF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Thai, 0x0E00, 0x0E7F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.TaiTham, 0x1A20, 0x1AAF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Sundanese, 0x1B80, 0x1BBF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.KayahLi, 0xA900, 0xA92F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Rejang, 0xA930, 0xA95F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Javanese, 0xA980, 0xA9DF),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.Cham, 0xAA00, 0xAA5F),
                              new UnicodeCategory(this[UnicodeIndex.SouthEastAsia], Strings.TaiViet, 0xAA80, 0xAADF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKUnifiedIdeographs, 0x4E00, 0x9FFF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKUnifiedIdeographsExtensionA, 0x3400, 0x4DBF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKCompatibilityIdeographs, 0xF900, 0xFAFF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKSymbolsAndPunctuation, 0x3000, 0x303F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.EnclosedCJKLettersAndMonths, 0x3200, 0x32FF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKCompatibility, 0x3300, 0x33FF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKCompatibilityForms, 0xFE30, 0xFE4F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.Kanbun, 0x3190, 0x319F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKRadicalsSupplement, 0x2E80, 0x2EFF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.KangxiRadicals, 0x2F00, 0x2FDF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.CJKStrokes, 0x31C0, 0x31EF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.IdeographicDescriptionCharacters, 0x2FF0, 0x2FFF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.Bopomofo, 0x3100, 0x312F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.BopomofoExtended, 0x31A0, 0x31BF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.Hiragana, 0x3040, 0x309F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.Katakana, 0x30A0, 0x30FF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.HangulSyllables, 0xAC00, 0xD7AF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.HangulJamo, 0x1100, 0x11FF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.HangulCompatibilityJamo, 0x3130, 0x318F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.HangulJamoExtendedA, 0xA960, 0xA97F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.HangulJamoExtendedB, 0xD7B0, 0xD7FF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.YiSyllables, 0xA000, 0xA48F),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.YiRadicals, 0xA490, 0xA4CF),
                              new UnicodeCategory(this[UnicodeIndex.EastAsia], Strings.Lisu, 0xA4D0, 0xA4FF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.CenterAsia], Strings.Mongolian, 0x1800, 0x18AF),
                              new UnicodeCategory(this[UnicodeIndex.CenterAsia], Strings.Phags_pa, 0xA840, 0xA87F),
                              new UnicodeCategory(this[UnicodeIndex.CenterAsia], Strings.Tibetan, 0x0F00, 0x0FFF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Glagolitic, 0x2C00, 0x2C5F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Ogham, 0x1680, 0x169F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Runic, 0x16A0, 0x16FF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Cherokee, 0x13A0, 0x13FF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.UnifiedCanadianAboriginalSyllabics, 0x1400, 0x167F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.UnifiedCanadianAboriginalSyllabicsExtended, 0x18B0, 0x18FF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.OldItalic, 0x10300, 0x103DF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Gothic, 0x10330, 0x1034F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Ugaritic, 0x10380, 0x1039F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.OldPersian, 0x103A0, 0x103DF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Osmanya, 0x10480, 0x104AF),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.CypriotSyllabary, 0x10800, 0x1083F),
                              new UnicodeCategory(this[UnicodeIndex.OtherLanguages], Strings.Phoenician, 0x10900, 0x1091F)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.GeneralPunctuation, 0x2000, 0x206F),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.YijingHexagramSymbols, 0x4DC0, 0x4DFF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.SupplementalPunctuation, 0x2E00, 0x2E7F),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.CurrencySymbols, 0x20A0, 0x20CF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.EnclosedAlphanumerics, 0x2460, 0x24FF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.MiscellaneousSymbols, 0x2600, 0x26FF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.Dingbats, 0x2700, 0x27BF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.BraillePatterns, 0x2800, 0x28FF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.ByzantineMusicalSymbols, 0x1D000, 0x1D0FF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.MusicalSymbols, 0x1D100, 0x1D1FF),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.AncientGreekMusicalNotation, 0x1D200, 0x1D24F),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.TaiXuanJingSymbols, 0x1D300, 0x1D37F),
                              new UnicodeCategory(this[UnicodeIndex.Symbols], Strings.CountingRodNumerals, 0x1D360, 0x1D37F)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.NumberForms, 0x2150, 0x218F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.SuperscriptsAndSubscripts, 0x2070, 0x209F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.LetterlikeSymbols, 0x2100, 0x214F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.Arrows, 0x2190, 0x21FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MathematicalOperators, 0x2200, 0x22FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.SupplementalMathematicalOperators, 0x2A00, 0x2AFF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MiscellaneousMathematicalSymbolsA, 0x27C0, 0x27EF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MiscellaneousMathematicalSymbolsB, 0x2980, 0x29FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.SupplementalArrowsA, 0x27F0, 0x27FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.SupplementalArrowsB, 0x2900, 0x297F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MiscellaneousSymbolsAndArrows, 0x2B00, 0x2BFF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.GeometricShapes, 0x25A0, 0x25FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.BoxDrawing, 0x2500, 0x257F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.BlockElements, 0x2580, 0x259F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.ControlPictures, 0x2400, 0x243F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MiscellaneousTechnical, 0x2300, 0x23FF),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.OpticalCharacterRecognition, 0x2440, 0x245F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.AegeanNumbers, 0x10100, 0x1013F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.AncientGreekNumbers, 0x10140, 0x1018F),
                              new UnicodeCategory(this[UnicodeIndex.Mathematics], Strings.MathematicalAlphanumericSymbols, 0x1D400, 0x1D7FF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.IPAExtensions, 0x0250, 0x02AF),
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.KatakanaPhoneticExtensions, 0x31F0, 0x31FF),
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.PhoneticExtensions, 0x1D00, 0x1D7F),
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.PhoneticExtensionsSupplement, 0x1D80, 0x1DBF),
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.SpacingModifierLetters, 0x02B0, 0x02FF),
                              new UnicodeCategory(this[UnicodeIndex.Phonetic], Strings.ModifierToneLetters, 0xA700, 0xA71F)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Diacritic], Strings.CombiningDiacriticalMarks, 0x0300, 0x036F),
                              new UnicodeCategory(this[UnicodeIndex.Diacritic], Strings.CombiningDiacriticalMarksSupplement, 0x1DC0, 0x1DFF),
                              new UnicodeCategory(this[UnicodeIndex.Diacritic], Strings.CombiningHalfMarks, 0xFE20, 0xFE2F),
                              new UnicodeCategory(this[UnicodeIndex.Diacritic], Strings.CombiningDiacriticalMarksForSymbols, 0x20D0, 0x20FF)
                          });
            categories.AddRange(new [] {
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.AlphabeticPresentationForms, 0xFB00, 0xFB4F),
                              //new UnicodeCategory(this[UnicodeIndex.Other], "Верхняя часть суррогатные пар", 0xD800, 0xDB7F),
                              //new UnicodeCategory(this[UnicodeIndex.Other], "Верхняя часть суррогатные пар для частного использования", 0xDB80, 0xDBFF),
                              //new UnicodeCategory(this[UnicodeIndex.Other], "Нижняя часть суррогатные пар", 0xDC00, 0xDFFF),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.PrivateUseArea, 0xE000, 0xF8FF),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.VariationSelectors, 0xFE00, 0xFE0F),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.VerticalForms, 0xFE10, 0xFE1F),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.Specials, 0xFFF0, 0xFFFF),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.HalfwidthAndFullwidthForms, 0xFF00, 0xFFEF),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.LinearBSyllabary, 0x10000, 0x1007F),
                              new UnicodeCategory(this[UnicodeIndex.Other], Strings.LinearBIdeograms, 0x10080, 0x100FF)
                          });
        }
    }
}
