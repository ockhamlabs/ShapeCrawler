﻿using DocumentFormat.OpenXml;
using ShapeCrawler.Placeholders;
using ShapeCrawler.SlideMasters;

namespace ShapeCrawler
{
    /// <summary>
    ///     Represents a shape located on Slide.
    /// </summary>
    internal abstract class SlideShape : Shape
    {
        protected SlideShape(OpenXmlCompositeElement childOfPShapeTree, SCSlide slide, Shape groupShape)
            : base(childOfPShapeTree, slide, groupShape)
        {
            this.Slide = slide;
        }

        protected SlideShape(OpenXmlCompositeElement childOfPShapeTree, SCSlide slide)
            : base(childOfPShapeTree, slide)
        {
            this.Slide = slide;
        }

        public override IPlaceholder Placeholder => SlidePlaceholder.Create(this.PShapeTreesChild, this);

        internal override SCSlideMaster SlideMasterInternal
        {
            get => (SCSlideMaster)this.Slide.ParentSlideLayout.ParentSlideMaster;

            set
            {
            }
        }

        public override SCPresentation PresentationInternal => this.Slide.PresentationInternal;

        public ISlide ParentSlide => this.Slide;

        internal SCSlide Slide { get; }
    }
}