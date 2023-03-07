namespace GI.BackOffice;

public static class ImageCropNamesConstants
{
    public static string Thumb = "thumb";
    public static string Quadratic = "quadratic";
    public static string Preview = "preview";
    public static string FeaturedPreview = "featuredPreview";

    public static string FromEnum(ImageCropName cropName)
    {
        switch (cropName)
        {
            case ImageCropName.Quadratic:
                return Quadratic;
            case ImageCropName.Preview:
                return Preview;
            case ImageCropName.Thumb:
                return Thumb;
            case ImageCropName.FeaturedPreview:
                return FeaturedPreview;
        }

        return "";
    }
}

public enum ImageCropName
{
    Default,
    Preview, // 500x300
    Thumb, //120x120
    Quadratic, //250x250
    FeaturedPreview, //500x220
}