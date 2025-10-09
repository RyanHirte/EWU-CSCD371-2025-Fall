using System;
using CanHazFunny;


//make a Jester object implementing the interfaces
//make a method to thell the joke
//joke = jokeService.GetJoke();
//make a while loop that filters out Chuck Norris jokes


public class Jester
{
    private InterfaceJokeService _JokeService { get; }
    private InterfaceOutputService _OutputService { get; }
    public Jester(InterfaceJokeService jokeService, InterfaceOutputService outputService)
    {
        _JokeService = jokeService;
        _OutputService = outputService;
    }
    public void TellJoke()
    {
        string joke = _JokeService.GetJoke();
        while (!joke.Contains("Chuck Norris"))
        {
            joke = _JokeService.GetJoke();
        }
        _OutputService.WriteJoke(joke);
    }
}