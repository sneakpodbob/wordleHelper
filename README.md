# wordleHelper

Little Windows-App to cheat when playing [wordle](https://www.powerlanguage.co.uk/wordle).

I'm in no way, shape or form associated with the [wordle](https://www.powerlanguage.co.uk/wordle) creator(s) - I'm just a fan who thought it might be a fun project to code a little helper tool, because I'm not a native english speaker and never seem to be able to come up with my next guess..

### Wordlist
It uses the [word-list](https://github.com/dwyl/english-words/blob/master/words_alpha.txt) from [dwyl/english-words](https://github.com/dwyl/english-words) - it reads the word-list from the included file - if you were to exchange that (keep the same name) it can use your own. The word-list is far from optimal, since it's not the same used in [wordle](https://www.powerlanguage.co.uk/wordle) - so sometimes, some of the suggestions cannot be used in [wordle](https://www.powerlanguage.co.uk/wordle). Their wordlist is not publicly available (AFAIK at least) - so it'll have to do. I removed some of the invalid words that always came out on top for the first suggestion from the list already, but there are many more. 

# Usage

![image](https://user-images.githubusercontent.com/4972863/150164375-25c94a70-d8b3-49bc-8207-6eaebb72d8a4.png)

Either enter your first guess into line one - or click FILTER to get suggestions.

If you want to use a suggestion, just double click it.

![image](https://user-images.githubusercontent.com/4972863/150164594-495523ed-8616-4ddd-9e24-72123c4f58ee.png)

Enter the same word in [wordle](https://www.powerlanguage.co.uk/wordle). Now *double click* the letters that returned yellow or green to change their colors in the helper, so it matches the [wordle](https://www.powerlanguage.co.uk/wordle) result.

![image](https://user-images.githubusercontent.com/4972863/150165083-040a9476-9117-4f75-b956-a7f83c3e4222.png)

Click evaluate again.

Choose a new suggestion and continue until you win (or lose - no guarantee here..)

## Example

If it works it looks like this:

![image](https://user-images.githubusercontent.com/4972863/150304726-2805028e-3ae2-4e39-b960-a8375c772b7c.png)

![image](https://user-images.githubusercontent.com/4972863/150304682-f90fd62e-23ce-46e5-9bf8-968512613503.png)

