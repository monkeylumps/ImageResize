using System;
using System.Linq;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
// This file is not unit tested as it has not business logic in  it and uses 3rd party libs that should be tested - 
// in prod work i would put the time in to test this but testing this library proved to be tricky
namespace ImageResize.Services
{
    public class ImageProcessService : IImageProcessService
    {
        public void AddWaterMark(Image image, string watermark)
        {
            var collection = new FontCollection();
            var family = collection.Install("Font/Ganttlets.ttf");
            var font = family.CreateFont(12, FontStyle.Italic);

            var imgSize = image.Size();

            var size = TextMeasurer.Measure(watermark, new RendererOptions(font));

            var scalingFactor = Math.Min(imgSize.Width / size.Width, imgSize.Height / size.Height);

            var scaledFont = new Font(font, scalingFactor * font.Size);

            var center = new PointF(imgSize.Width / 2, imgSize.Height / 2);
            var textGraphicOptions = new TextGraphicsOptions()
            {
                TextOptions = {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            image.Mutate(context => context.DrawText(textGraphicOptions, watermark, scaledFont, Color.Aqua, center));
        }

        public void ChangeBackgroundColour(Image image, string colourHex)
        {
            image.Mutate(context => context.BackgroundColor(Color.ParseHex(colourHex)));
        }

        public void ResizeImage(Image image, string resolution)
        {
            var heightAndWidth = resolution.Split("x");
            var height = int.Parse(heightAndWidth.Last());
            var width = int.Parse(heightAndWidth.First());

            image.Mutate(context =>
                context.Resize(width, height));
        }
    }
}
