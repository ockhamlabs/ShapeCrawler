﻿using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using PptxXML.Enums;
using PptxXML.Extensions;
using PptxXML.Services;
using Xunit;
using P = DocumentFormat.OpenXml.Presentation;

namespace PptxXML.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ElementCreator"/> class.
    /// </summary>
    public class ElementCreatorTests
    {
        [Fact]
        public void CreateShape_Test()
        {
            // ARRANGE
            var ms = new MemoryStream(Properties.Resources._009);
            var doc = PresentationDocument.Open(ms, false);
            var stubXmlShape = doc.PresentationPart.SlideParts.Single().Slide.CommonSlideData.ShapeTree.Elements<P.Shape>().Single(s => s.GetId() == 36);
            var stubEc = new ElementCandidate
            {
                CompositeElement = stubXmlShape,
                ElementType = ElementType.Shape
            };
            var creator = new ElementCreator();

            // ACT
            var element = creator.CreateShape(stubEc);

            // CLEAN
            doc.Dispose();
            ms.Dispose();

            // ASSERT
            Assert.Equal(ElementType.Shape, element.Type);
            Assert.Equal(3291840, element.X);
            Assert.Equal(274320, element.Y);
            Assert.Equal(1143000, element.Width);
            Assert.Equal(1143000, element.Height);
        }

        [Fact]
        public void CreatePicture_Test()
        {
            // ARRANGE
            var ms = new MemoryStream(Properties.Resources._009);
            var doc = PresentationDocument.Open(ms, false);
            var stubXmlPic = doc.PresentationPart.SlideParts.Single().Slide.CommonSlideData.ShapeTree.Elements<P.Picture>().Single();
            var stubEc = new ElementCandidate
            {
                CompositeElement = stubXmlPic,
                ElementType = ElementType.Picture
            };
            var creator = new ElementCreator();

            // ACT
            var element = creator.CreatePicture(stubEc);

            // CLEAN
            doc.Dispose();
            ms.Dispose();

            // ASSERT
            Assert.Equal(ElementType.Picture, element.Type);
            Assert.Equal(4663440, element.X);
            Assert.Equal(1005840, element.Y);
            Assert.Equal(2315880, element.Width);
            Assert.Equal(2315880, element.Height);
        }

        [Fact]
        public void CreateTable_Test()
        {
            // ARRANGE
            var ms = new MemoryStream(Properties.Resources._009);
            var doc = PresentationDocument.Open(ms, false);
            var stubGrFrame = doc.PresentationPart.SlideParts.Single().Slide.CommonSlideData.ShapeTree.Elements<P.GraphicFrame>().Single(e => e.GetId() == 38);
            var stubEc = new ElementCandidate
            {
                CompositeElement = stubGrFrame,
                ElementType = ElementType.Table
            };
            var creator = new ElementCreator();

            // ACT
            var element = creator.CreateTable(stubEc);

            // CLEAN
            doc.Dispose();
            ms.Dispose();

            // ASSERT
            Assert.Equal(ElementType.Table, element.Type);
            Assert.Equal(453240, element.X);
            Assert.Equal(3417120, element.Y);
            Assert.Equal(5075640, element.Width);
            Assert.Equal(1439640, element.Height);
        }

        [Fact]
        public void CreateChart_Test()
        {
            // ARRANGE
            var ms = new MemoryStream(Properties.Resources._009);
            var doc = PresentationDocument.Open(ms, false);
            var stubGrFrame = doc.PresentationPart.SlideParts.Single().Slide.CommonSlideData.ShapeTree.Elements<P.GraphicFrame>().Single(x=>x.GetId() == 4);
            var stubEc = new ElementCandidate
            {
                CompositeElement = stubGrFrame,
                ElementType = ElementType.Chart
            };
            var creator = new ElementCreator();

            // ACT
            var element = creator.CreateChart(stubEc);

            // CLEAN
            doc.Dispose();
            ms.Dispose();

            // ASSERT
            Assert.Equal(ElementType.Chart, element.Type);
            Assert.Equal(453241, element.X);
            Assert.Equal(752401, element.Y);
            Assert.Equal(2672732, element.Width);
            Assert.Equal(1819349, element.Height);
        }
    }
}
