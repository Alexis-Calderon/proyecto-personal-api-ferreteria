namespace ferreteriaJuanito;

// Modelo de datos para las excepciones globales del sistema.
public class GlobalException
{
    public int StatusCode { get; }
    public string Message { get; }
    public string Detaild { get; }

    public GlobalException(int statusCode, string message = null, string detaild = null)
    {
        StatusCode = statusCode;
        Message = message;
        Detaild = detaild;
    }
}