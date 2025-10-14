using System;

namespace CanHazFunny;

public class Jester
{
    private IJokeService JokeService { get; }
    private IOutputService OutputService { get; }
    
    public Jester(IJokeService jokeService, IOutputService outputService)
    {
        JokeService = jokeService ?? throw new ArgumentNullException(nameof(jokeService));
        OutputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
    }

    public void TellJoke()
    {
        string joke = JokeService.GetJoke();
        while (joke != null && joke.IndexOf("Chuck Norris", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            joke = JokeService.GetJoke();
        }
        OutputService.WriteJoke(joke ?? string.Empty);
    }
}