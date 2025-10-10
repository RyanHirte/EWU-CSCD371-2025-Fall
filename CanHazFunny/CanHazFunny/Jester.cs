using System;
using CanHazFunny;


public class Jester
{
    private InterfaceJokeService JokeService { get; }
    private InterfaceOutputService OutputService { get; }
    
    public Jester(InterfaceJokeService jokeService, InterfaceOutputService outputService)
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