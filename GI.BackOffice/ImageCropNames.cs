namespace GI.BackOffice;

public static class ImageCropNamesConstants
{
    public static string Thumb = "thumb";
    public static string Quadratic = "quadratic";
    public static string Preview = "preview";
    public static string FeaturedPreview = "featuredPreview";
    public static string MainImage = "mainImage";

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
            case ImageCropName.MainImage:
                return MainImage;
        }

        return "";
    }
}

public enum ImageCropName
{
    Default,
    Preview, 
    Thumb, 
    Quadratic,
    FeaturedPreview,
    MainImage,
}