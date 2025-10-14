namespace CanHazFunny;

// interface for retrieving jokes 
public interface IJokeService
{
    // retrieves the joke and returns it in a string
    string GetJoke();
}