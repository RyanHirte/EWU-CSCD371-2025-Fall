using System;
using CanHazFunny;

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
        while (joke.Contains("Chuck Norris"))
        {
            joke = JokeService.GetJoke();
        }
        OutputService.WriteJoke(joke);
    }
}