using System;


//make a Jester object implementing the interfaces
//make a method to thell the joke
//joke = jokeService.GetJoke();
//make a while loop that filters out Chuck Norris jokes


public class Jester
{
    private InterfaceJokeService JokeService { get; }
    private InterfaceOutputService OutputService { get; }
    public Jester(InterfaceJokeService jokeService, InterfaceOutputService outputService)
    {
        JokeService = jokeService;
        OutputService = outputService;
    }
    public void TellJoke()
    {
        string joke = JokeService.GetJoke();
        while (!joke.Contains("Chuck Norris"))
        {
            joke = JokeService.GetJoke();
        }
        OutputService.WriteJoke(joke);
    }

}