using System;
using System.Linq;
using Xunit;
using mxcd.util.text;
using static mxcd.util.enums.UtilEnums;

namespace mxcd.util.test
{
    public class Text
    {
        [Fact]
        public void ToSentence()
        {
            Assert.True("HolaHolaHola".ToSentence() == "Hola Hola Hola");
            Assert.True("".ToSentence() == "");
            Assert.True("BBB".ToSentence() == "BBB");
        }

        [Fact]
        public void GetLast()
        {
            Assert.True("UnoDosTres".Last(4) == "Tres");
            Assert.True("".Last(4) == "");
        }

        [Fact]
        public void GetFirst()
        {
            Assert.True("UnoDosTres".First(3) == "Uno");
            Assert.True("".First(3) == "");
        }

        [Fact]
        public void Random()
        {
            var text = "UnoDosTres";
            var newTxt = "UnoDosTres".Random();
            Assert.True(text.Length == newTxt.Length && text != newTxt);
        }

        [Fact]
        public void IsEmail()
        {
            Assert.True("mail@corre.com".Check().IsEmail());
            Assert.False("mail@corre".Check().IsEmail());
            Assert.False("".Check().IsEmail());
            Assert.False("mail @corre.com".Check().IsEmail());
            Assert.False("mailcorre.com".Check().IsEmail());
        }

        [Fact]
        public void IsPhone()
        {
            Assert.False("9999".Check().IsPhone());
            Assert.True("915555555".Check().IsPhone());
        }

        [Fact]
        public void IsMobile()
        {
            Assert.False("9999".Check().IsMobile());
            Assert.False("915555555".Check().IsMobile());
            Assert.True("616451328".Check().IsMobile());

        }

        [Fact]
        public void FormatWithMask()
        {
            Assert.True("holacomoestas".Check().IsFormatWithMask("#### #### #####") == "hola como estas");
            Assert.True("".Check().IsFormatWithMask("#### #### #####") == "");
            Assert.True("holacomoestas".Check().IsFormatWithMask("#### #### ##########") == "hola como estas");
            Assert.True("holacomoestas".Check().IsFormatWithMask("FORMATEADO: #### #### ##########") == "FORMATEADO: hola como estas");
        }

        [Fact]
        public void RemoveTags()
        {
            Assert.True("<p>Texto que se mantiene.</p>".Html().RemoveTags() == " Texto que se mantiene. ");
            Assert.True("<p>Texto que se mantiene.<span class='bold'>Negrita.</span>Valores</p>".Html().RemoveTags() == " Texto que se mantiene. Negrita. Valores ");
            Assert.True("".Html().RemoveTags() == "");
        }

        [Fact]
        public void ContainsAny()
        {
            Assert.True("abcnedkslqjifhsmadufkqks".ContainsAny(new char[] { 's', 'm' }));
            Assert.True(!"abcnedkslqjifhsmadufkqks".ContainsAny(new char[] { 'z' }));
            Assert.True(!"".ContainsAny(new char[] { 'z', 'a' }));
        }

        [Fact]
        public void IsIsin()
        {
            Assert.True("AU0000XVGZA3".Check().IsIsin());
            Assert.False("AU0000XVGZD3".Check().IsIsin());
            Assert.False("".Check().IsIsin());
        }

        [Fact]
        public void IsValidUrl()
        {
            Assert.True("http://www.google.es".Check().IsUrl());
            Assert.True("http://www.google".Check().IsUrl());
            Assert.False("httpas://www.google".Check().IsUrl());
        }

        [Fact]
        public void Repeat()
        {
            Assert.True("HOLA".Repeat(3) == "HOLAHOLAHOLA");
            Assert.True("".Repeat(4) == "");
        }

        [Fact]
        public void IndicesDe()
        {
            Assert.Equal("babbab".GetIndexOf("b"), new[] { 0, 2, 3, 5 });
        }

        [Fact]
        public void UFT8()
        {
            var Texto = "mxcd mola";

            var bytes = Texto.ToBytes(Encode.UTF8);
            Assert.True(Texto == bytes.ToString(Encode.UTF8));
        }

        [Fact]
        public void Base64()
        {
            var Texto = System.Convert.ToBase64String("mxcd mola".ToBytes(Encode.UTF8));

            var bytes = Texto.ToBytes(Encode.UTF8);
            Assert.True(Texto == bytes.ToString(Encode.UTF8));
        }

        [Fact]
        public void ASCII()
        {
            var Texto = "mxcd mola";

            var bytes = Texto.ToBytes(Encode.ASCII);
            Assert.True(Texto == bytes.ToString(Encode.ASCII));
        }
    }
}
