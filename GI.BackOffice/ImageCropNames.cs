namespace GI.BackOffice;

public static class ImageCropNamesConstants
{
    public static string Thumb = "thumb";
    public static string Quadratic = "quadratic";
    public static string Preview = "preview";

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
}