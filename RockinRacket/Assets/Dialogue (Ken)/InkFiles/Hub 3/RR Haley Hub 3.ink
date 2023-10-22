INCLUDE ../globals.ink

-> H3

=== H3 ===
= Haley
#speaker: Haley #portrait: Haley_neutral
What do you want Harvey?
 * [Can we please talk?]
        #speaker: Harvey #portrait: Harvey_neutral
        Can we please talk?
        
        -> H_H3_C1
            
 * [End convo]
        -> END


= H_H3_C1
#speaker: Haley #portrait: Haley_neutral
What is there to talk about? Maybe about how you were just using us?
    * [I know it seemed like that, but...]
        #speaker: Harvey #portrait: Harvey_neutral
        I know it seemed like that, but I promise that I wasn't doing that the entire time.
        
        #speaker: Haley #portrait: Haley_neutral
        Even if I choose to believe you, the fact is that you still used us even if it was for a short period of time.
        
            -> END
    * [I was in the beginning, but...]
        #speaker: Harvey #portrait: Harvey_neutral
        I was in the beginning, but I swear I stopped caring about the application soon after. I grew to love being with, and playing gigs with, you guys.
        
        #speaker: Haley #portrait: Haley_neutral
        Well no matter the case, you should have told us about it sooner. You hurt all of us Harvey. You've caused pain that you probably can't heal.
        
            //Change this line?
            -> END