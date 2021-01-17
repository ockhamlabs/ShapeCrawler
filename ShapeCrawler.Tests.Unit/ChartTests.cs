﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using ShapeCrawler.Charts;
using ShapeCrawler.Enums;
using ShapeCrawler.Tests.Unit.Helpers;
using Xunit;

// ReSharper disable TooManyDeclarations
// ReSharper disable InconsistentNaming
// ReSharper disable TooManyChainedReferences

namespace ShapeCrawler.Tests.Unit
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    public class ChartTests : IClassFixture<PptxFixture>
    {
        private readonly PptxFixture _fixture;

        public ChartTests(PptxFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void XValues_ReturnsParticularXAxisValue_ViaItsCollectionIndexer()
        {
            // Arrange
            ChartSc chart = _fixture.Pre024.Slides[1].Shapes.First(sp => sp.Id == 5).Chart;

            // Act
            double xValue = chart.XValues[0];

            // Assert
            xValue.Should().Be(10);
            chart.HasXValues.Should().BeTrue();
        }


        [Fact]
        public void HasXValues()
        {
            // Arrange
            var sld1 = _fixture.Pre025.Slides[0];
            var sld2 = _fixture.Pre025.Slides[1];
            var chart8 = sld1.Shapes.First(x => x.Id == 8).Chart;
            var chart11 = sld2.Shapes.First(x => x.Id == 11).Chart;

            // Act
            var chart8HasXValues = chart8.HasXValues;
            var chart11HasXValues = chart11.HasXValues;

            // Assert
            Assert.False(chart8HasXValues);
            Assert.False(chart11HasXValues);
        }

        [Fact]
        public void HasCategories_ReturnsFalse_WhenAChartHasNotCategories()
        {
            // Arrange
            ChartSc chart = _fixture.Pre021.Slides[2].Shapes.First(sp => sp.Id == 4).Chart;

            // Act
            bool hasChartCategories = chart.HasCategories;

            // Assert
            hasChartCategories.Should().BeFalse();
        }

        [Fact]
        public void TitleAndHasTitle_ReturnChartTitleStringAndFlagIndicatingWhetherChartHasATitle()
        {
            // Arrange
            ChartSc chartCase1 = _fixture.Pre018.Slides[0].Shapes.First(sp => sp.Id == 6).Chart;
            ChartSc chartCase2 = _fixture.Pre025.Slides[0].Shapes.First(sp => sp.Id == 7).Chart;
            ChartSc chartCase3 = _fixture.Pre013.Slides[0].Shapes.First(sp => sp.Id == 5).Chart;
            ChartSc chartCase4 = _fixture.Pre013.Slides[0].Shapes.First(sp => sp.Id == 4).Chart;
            ChartSc chartCase5 = _fixture.Pre019.Slides[0].Shapes.First(sp => sp.Id == 4).Chart;
            ChartSc chartCase6 = _fixture.Pre013.Slides[0].Shapes.First(sp => sp.Id == 6).Chart;

            // Act
            string charTitleCase1 = chartCase1.Title;
            string charTitleCase2 = chartCase2.Title;
            string charTitleCase3 = chartCase3.Title;
            string charTitleCase5 = chartCase5.Title;
            bool hasTitleCase4 = chartCase4.HasTitle;
            bool hasTitleCase6 = chartCase6.HasTitle;

            // Assert
            charTitleCase1.Should().BeEquivalentTo("Test title");
            charTitleCase2.Should().BeEquivalentTo("Series 1");
            charTitleCase3.Should().BeEquivalentTo("Title text");
            charTitleCase5.Should().BeEquivalentTo("Test title");
            hasTitleCase4.Should().BeFalse();
            hasTitleCase6.Should().BeFalse();
        }

        [Fact]
        public void Chart_Test()
        {
            // Arrange
            var pre = _fixture.Pre021;
            var shapes1 = pre.Slides[0].Shapes;
            var shapes2 = pre.Slides[1].Shapes; // TODO: Research why this statement takes mach time
            var sp108 = shapes1.First(x => x.Id == 108);
            var chart3 = shapes1.First(x => x.Id == 3).Chart;
            var sld2Chart4 = shapes2.First(x => x.Id == 4).Chart;
            var lineChartSeries = sld2Chart4.SeriesCollection[1];

            // Act
            var fill = sp108.Fill; //assert: do not throw exception

            var barChartPointValue = chart3.SeriesCollection[1].PointValues[0];
            var scatterChartPointValue = chart3.SeriesCollection[2].PointValues[0];
            var lineChartPointValue = lineChartSeries.PointValues[0];

            // Assert
            Assert.Equal(56, barChartPointValue);
            Assert.Equal(44, scatterChartPointValue);
            Assert.Equal(17.35, lineChartPointValue);
        }

        [Fact]
        public void CategoryName_ContainsNameOfMainOrSubcategory()
        {
            ChartSc chartCase1 = _fixture.Pre025.Slides[0].Shapes.First(sp => sp.Id == 4).Chart;
            ChartSc chartCase2 = _fixture.Pre021.Slides[0].Shapes.First(sp => sp.Id == 4).Chart;

            // Act
            string mainNameOfMultiCategory = chartCase1.Categories[0].MainCategory.Name;
            string subNameOfMultiCategory = chartCase1.Categories[0].Name;
            string nameOfNonMultiCategory = chartCase2.Categories[0].Name;

            // Assert
            mainNameOfMultiCategory.Should().BeEquivalentTo("Clothing");
            subNameOfMultiCategory.Should().BeEquivalentTo("Dresses");
            nameOfNonMultiCategory.Should().BeEquivalentTo("2015");
        }

        [Fact]
        public void SeriesType_ReturnsChartTypeOfTheSeries()
        {
            // Arrange
            ChartSc chart = _fixture.Pre021.Slides[0].Shapes.First(sp => sp.Id == 3).Chart;
            Series series2 = chart.SeriesCollection[1];
            Series series3 = chart.SeriesCollection[2];

            // Act
            ChartType seriesChartType2 = series2.Type;
            ChartType seriesChartType3 = series3.Type;

            // Assert
            seriesChartType2.Should().Be(ChartType.BarChart);
            seriesChartType3.Should().Be(ChartType.ScatterChart);
        }

        [Fact]
        public void SeriesCollection_CounterReturnsNumberOfTheSeriesInTheChart()
        {
            // Arrange
            ChartSc chart = _fixture.Pre013.Slides[0].Shapes.First(sp => sp.Id == 5).Chart;
            
            // Act
            int seriesCount = chart.SeriesCollection.Count;

            // Assert
            seriesCount.Should().Be(3);
        }

        [Fact]
        public void SeriesPointValue_ReturnsChartSeriesPointValue()
        {
            // Arrange
            Series seriesCase1 = _fixture.Pre021.Slides[1].Shapes.First(sp => sp.Id == 3).Chart.SeriesCollection[0];
            Series seriesCase2 = _fixture.Pre021.Slides[2].Shapes.First(sp => sp.Id == 4).Chart.SeriesCollection[0];
            Series seriesCase3 = _fixture.Pre025.Slides[1].Shapes.First(sp => sp.Id == 4).Chart.SeriesCollection[0];

            // Act
            double seriesPointValueCase1 = seriesCase1.PointValues[0];
            double seriesPointValueCase2 = seriesCase2.PointValues[0];
            double seriesPointValueCase3 = seriesCase3.PointValues[0];

            // Arrange
            seriesPointValueCase1.Should().Be(20.4);
            seriesPointValueCase2.Should().Be(2.4);
            seriesPointValueCase3.Should().Be(72.7);
        }

        [Fact]
        public void SeriesName_ReturnsChartSeriesName()
        {
            // Arrange
            ChartSc chart = _fixture.Pre025.Slides[0].Shapes.First(sp => sp.Id == 5).Chart;

            // Act
            string seriesNameCase1 = chart.SeriesCollection[0].Name;
            string seriesNameCase2 = chart.SeriesCollection[2].Name;

            // Assert
            seriesNameCase1.Should().BeEquivalentTo("Ряд 1");
            seriesNameCase2.Should().BeEquivalentTo("Ряд 3");
        }

        [Fact]
        public void Type_ReturnsChartType()
        {
            // Arrange
            ChartSc chartCase1 = _fixture.Pre021.Slides[1].Shapes.First(sp => sp.Id == 3).Chart;
            ChartSc chartCase2 = _fixture.Pre021.Slides[2].Shapes.First(sp => sp.Id == 4).Chart;
            ChartSc chartCase3 = _fixture.Pre013.Slides[0].Shapes.First(sp => sp.Id == 5).Chart;

            // Act
            ChartType chartTypeCase1 = chartCase1.Type;
            ChartType chartTypeCase2 = chartCase2.Type;
            ChartType chartTypeCase3 = chartCase3.Type;

            // Assert
            chartTypeCase1.Should().Be(ChartType.BubbleChart);
            chartTypeCase2.Should().Be(ChartType.ScatterChart);
            chartTypeCase3.Should().Be(ChartType.Combination);
        }
    }
}