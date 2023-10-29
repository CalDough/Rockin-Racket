INCLUDE ../globals.ink

-> H1

=== H1 ===
= Kurt
#speaker: Kurt #portrait: Kurt_neutral
Oh, h-h-h-hey Harvey. Do you n-n-need something?
    * [Just checking in]
        #speaker: Harvey #portrait: Harvey_neutral
        Just checking in. Are you looking for something?
        
        -> K_H1_C1
    * [Yeah, I wanted to check...]
        #speaker: Harvey #portrait: Harvey_neutral
        Yeah, I wanted to check if your bass guitar was okay?
        
        -> K_H1_C2
    * [End convo]
        -> END

= K_H1_C1
#speaker: Kurt #portrait: Kurt_neutral
Y-y-yeah, I am. I lost that l-l-little case I put my guitar p-p-picks in. I thought I left if on the shelf but it's n-n-not there anymore.
    * [That sucks]
        #speaker: Harvey #portrait: Harvey_neutral
        That sucks. I'm sure it's bound to show back up eventually.
        
        #speaker: Kurt #portrait: Kurt_neutral
        I know, I'm j-j-just stressed about it.
        
            -> END
    * [Have you thought about checking...]
        #speaker: Harvey #portrait: Harvey_neutral
        Have you thought about checking in with MJ? Maybe she took it?
        
        #speaker: Kurt #portrait: Kurt_neutral
        You have a p-p-point. Thanks!
        
            -> END

//Fix this section
= K_H1_C2
#speaker: Kurt #portrait: Kurt_neutral
I don't think I absolutely need one right now, but it would be awesome if I did. It would sound so much better.
    * [I can get it]
        #speaker: Harvey #portrait: Harvey_neutral
        I can get it for you here soon.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Best manager in the making right here.
        
            -> END
    * [I'll think about it]
        #speaker: Harvey #portrait: Harvey_neutral
        I'll think about it. I obviously don't want to prioritize yours if someone else needs new gear more than you.
        
        #speaker: Kurt #portrait: Kurt_neutral
        It's all good. Thanks for thinking about me though.
        
            -> END