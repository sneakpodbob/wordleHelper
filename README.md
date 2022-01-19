# wordleHelper

WinForms based Tool to cheat when playing [wordle](https://www.powerlanguage.co.uk/wordle).
(I'm in no way shape or form associated with the wordle creator(s) - I'm just a fan who thought it might be a fun project to code a little helper tool, because I'm not a native english speaker and never seem to be able to come up with my next guess..)

It uses the [word-list](https://github.com/dwyl/english-words/blob/master/words_alpha.txt) from [dwyl/english-words](https://github.com/dwyl/english-words) - it reads the word-list from the included file - if you were to exchange that (keep the same name) it can use your own. The word-list is far from optimal, since it's not the same used in wordle - so sometimes, some of the suggestions cannot be used in wordle. Their wordlist is not publicly available (AFAIK at least) - so it'll have to do.

Usage:

![image](https://user-images.githubusercontent.com/4972863/150164375-25c94a70-d8b3-49bc-8207-6eaebb72d8a4.png)

Either enter your first guess into line one - or click FILTER to get suggestions.

If you want to use a suggestion, just double click it.

![image](https://user-images.githubusercontent.com/4972863/150164594-495523ed-8616-4ddd-9e24-72123c4f58ee.png)

Enter the same word on wordle and *double click* the letters that returned yellow or green to change their color, so it matches the wordle result.

![image](https://user-images.githubusercontent.com/4972863/150165083-040a9476-9117-4f75-b956-a7f83c3e4222.png)

Click evaluate again.

Choose a new suggestion and continue until you win (or lose - no guarantee here..)
