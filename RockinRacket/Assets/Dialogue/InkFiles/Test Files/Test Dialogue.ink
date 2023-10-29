INCLUDE ../globals.ink

-> First_Phrase  

=== First_Phrase ===
    Hello! This is a test script. #speaker: MJ #portrait: MJ_neutral
    If you see this line, then the continue button has worked!

    Alright, now let's check if the dialogue options work.
    {color_chosen == "": -> Question1 | -> Question1Chosen}
    
= Question1
    Let's start with a simple question!
    What is your favorite color?
    * Red
        -> Question1End("Red")
    * Blue
        -> Question1End("Blue")
    * Green
        -> Question1End("Green")
    * Purple
        -> Question1End("Purple")

= Question1End(color) 
    ~color_chosen = color
    Awesome {color} is my favorite color too!
    Alright, next simple question.
    -> Question2

= Question1Chosen
    First, what's your fav... wait!
    I already know your favorite color is {color_chosen}.
    Different question!
    -> Question2

= Question2    
    What is the meaning of life?
    * 42 
        A wise guy "Hitchhiker's Guide to the Galaxy" fan eh? Well that doesn't answer it this time!
        -> Question2
    * The pursuit of love[] and happiness. 
        No, why would that be the case?
        -> Question2
    * An endless grind[] against the powers at play for the mere hope of a marginally brighter future for those that follow.
        Woah there! Nihilistic much? You're killing the testing vibe.
        -> Question2
    * ->
        Alright, you've exhausted all options, so I guess I can say that you have successfully chosen all options!
        Also there is no one singular answer to that question that fits, so it was a bit rude to ask in the first place.
        -> QuestionwithManyOptions

= QuestionwithManyOptions
    Now for another question, this time to test the scrolling of options when it needs it.
    ->QuestionStart
    
    = QuestionStart
    How many options are there for this question?
    * 1
        Wrong! -> QuestionStart
    * 2
        Wrong! -> QuestionStart
    * 3
        Wrong! -> QuestionStart
    * 4
        Wrong! -> QuestionStart
    * 5
        Wrong! -> QuestionStart
    * 6
        Correct! -> QuestionEnd
    
    = QuestionEnd
    Can you tell I ran out of ideas for that question because I have been staring at a computer screen for 6 hours now?
    Anyway, enough of my complaining, it is a bit aggravating. 
    ->TestEnd

= TestEnd
    However, now you have successfully exhausted a dialogue, well done!
->END