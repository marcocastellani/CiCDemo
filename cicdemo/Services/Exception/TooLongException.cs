namespace cicdemo.Services;

public class TooLongException : Exception
{
    public TooLongException(int maxLenght): base("Message is too long, limit is " + maxLenght)
    {
        
    }

     
}