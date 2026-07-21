namespace DevPulse.Models;  // namespace is a way to organize code into logical groups 
// keeps model classes organized/grouped so other files can reference them without conflicts


public class CardResult<T> //Generic class that can be used with any type of data  
//Think about the automotive analogy
//same engine can be used in different cars   
{

    public T? Data {get; set;}
    public string? Error {get; set;} 
    public bool Ok => Error is null && Data is not null;
}
