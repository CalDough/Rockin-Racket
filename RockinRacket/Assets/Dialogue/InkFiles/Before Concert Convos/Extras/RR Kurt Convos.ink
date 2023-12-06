INCLUDE ../globals.ink

//Naming - ex. H_H1_C1 (first letter (the H in this case) stands for the character (Haley in this case), the H1 stands for which hub it is in (Hub 1 in this case), and C1 is the dialogue choice)

-> H1

=== Ending === ///This is just a place for the conversations to route back to so there are no errors
This is a placeholder
    * [...]
        -> END


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

= Kurt_option2_2

-> END


=== H2 ===
= Kurt
#speaker: Kurt #portrait: Kurt_neutral
Hi there H-h-harvey. Can I help you?
    * [Got any advice for me?]
        #speaker: Harvey #portrait: Harvey_neutral
        Got any advice for me? I feel like I'm starting to get the hang of being a stage manager, but I wanted to know if there was anything else to know?
        
        -> K_H2_C1
    * [Is your bass okay?]
        #speaker: Harvey #portrait: Harvey_neutral
        Is your bass okay or is there something I should know about?
        
        -> K_H2_C2
    * [End convo]
        -> END

= K_H2_C1
#speaker: Kurt #portrait: Kurt_neutral
I d-d-don't think so. Just p-p-pay attention to both the st-st-stage and the audience. Make the experience a-a-amazing for them.
    * [You have a good point]
        #speaker: Harvey #portrait: Harvey_neutral
        You have a good point. It's just a bit hectic, ya know?
        
        #speaker: Kurt #portrait: Kurt_neutral
        We d-d-did warn you. But you're doing f-f-fine so far so don't worry about i-i-it.
        
            -> END
    * [They can be really energetic]
        #speaker: Harvey #portrait: Harvey_neutral
        They can be really energetic and loud sometimes so I doubt I'll miss anything from them.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Agreed. I worry about the d-d-day they are louder than u-u-us though because that would b-b-be t-t-terrifying.
        
            -> END

= K_H2_C2
#speaker: Kurt #portrait: Kurt_neutral
Now that you mention it, yeah. The neck of it is starting to crack.
    * [I'll take care of it]
        #speaker: Harvey #portrait: Harvey_neutral
        I'll take care of it and get a new one here soon then.
        
        #speaker: Kurt #portrait: Kurt_neutral
        You're the best!
        
            -> END
    * [Give me some time]
        #speaker: Harvey #portrait: Harvey_neutral
        Give me some time. Funds are low right now so hopefully it can last a bit longer.
        
        #speaker: Kurt #portrait: Kurt_neutral
        It should last a few more gigs as long as I'm careful.
        
            -> END


-> END

=== H3 ===
= Kurt
#speaker: Kurt #portrait: Kurt_neutral
P-p-please don't tell me you're here t-t-to hurt me again?
    * [No, I'm not]
        #speaker: Harvey #portrait: Harvey_neutral
        No, I'm not Kurt. I wanted to say that I'm sorry for the pain I caused you. I know what I did was bad, but you have to believe me when I say that I changed.
        
        -> K_H3_C1
    * [End convo]
        -> END


= K_H3_C1
#speaker: Kurt #portrait: Kurt_neutral
A-a-apoligizing won't heal the p-p-pain. I'm more upset with m-m-myself for believing you when you offered t-t-to help us. You hate that k-kind of thing.
    * [This time was different]
        #speaker: Harvey #portrait: Harvey_neutral
        This time was different. I really started to love helping you guys. It gave me something to look forward to.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Well n-n-now that's gone, so way to g-g-go.
        
            -> END
    * [Please don't beat yourself up...]
        #speaker: Harvey #portrait: Harvey_neutral
        Please don't beat yourself up about it, it's my fault. I didn't want to risk telling you guys so I thought if I ignored it then it would all be okay.
        
        #speaker: Kurt #portrait: Kurt_neutral
        You're m-m-mistake was keeping the ap-p-plication. You should have t-t-tossed it out.
        
            -> END

=== H4 ===
= Kurt
#speaker: Kurt #portrait: Kurt_neutral
Ace said you g-g-guys could use some h-h-help.
    * [I appreciate it]
        #speaker: Harvey #portrait: Harvey_neutral
        I appreciate it, but was that the only reason?
        
        #speaker: Kurt #portrait: Kurt_neutral
        Well, I c-c-couldn't stay mad at you f-f-forever. You're our big b-b-brother.
        
        #speaker: Harvey #portrait: Harvey_neutral
            Ahh come here. Let me give you a big hug.
            
        -> END
    * [We can really use it]
        #speaker: Harvey #portrait: Harvey_neutral
        We can really use it. Was that all it took to change your mind though?
        
        #speaker: Kurt #portrait: Kurt_neutral
        P-p-pretty much. I missed having you a-a-around, even if you were only g-g-gone for a short while.
        
        #speaker: Harvey #portrait: Harvey_neutral
            What would I ever do without you Kurt?
            
        -> END


=== Convo_2 ===


= Kurt
#speaker: Kurt #portrait: Kurt_neutral
Hey Harvey! I'm still c-c-cleaning them, but how do m-m-my figurines look right now?
    * [You have figurines?]
        #speaker: Harvey #portrait: Harvey_neutral
        You have figurines? I didn't know that. What are they of?
        
        -> K_Convo2_C1
    * [Aren't you a little old...]
        #speaker: Harvey #portrait: Harvey_neutral
        Aren't you a little old to have figurines?
        
        -> K_Convo2_C2
        
= K_Convo2_C1
#speaker: Kurt #portrait: Kurt_neutral
They're from my f-f-favorite show, The Last Mousekateer. I have f-figurines of J-jax, Ruffy, Marge, Cantor, and L-l-leonidas so far. I'm hoping to get one of P-p-pedro soon though.
    * [Oh wow]
        #speaker: Harvey #portrait: Harvey_neutral
        Oh wow, you have a bunch then. That's pretty cool Kurt.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Thank you! It just finished its f-f-forth season so it's on a b-b-break right now, so I can show it to you l-l-later if you'd like. Just let me know i-i-if you do!
        
            ** [Continue]
                -> Convo_2
    * [I'm glad]
        #speaker: Harvey #portrait: Harvey_neutral
        I'm glad that you are enjoying it. I'll leave you to it then.
        
        #speaker: Kurt #portrait: Kurt_neutral
        T-t-thanks! Oh! Can you take this old d-d-duster with you? It b-b-broke on me.
        
            ** [Continue]
                -> Convo_2

= K_Convo2_C2
#speaker: Kurt #portrait: Kurt_neutral
You c-c-can never be too old to enjoy anything that m-m-makes you happy. Don't you have s-s-something you still enjoy?
    * [I mean, I guess]
        #speaker: Harvey #portrait: Harvey_neutral
        I mean, I guess I do still kind of like some old school bands from when I was a kid.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Well th-th-there you go, we're not so d-d-different then. You haven't outgrown them in the s-s-same way I haven't outgrown my show.
        
            ** [Continue]
                -> Convo_2
    * [Nope]
        #speaker: Harvey #portrait: Harvey_neutral
        Nope. I got nothing.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Well that's just s-s-sad. You gotta find something to enjoy or else you're g-g-gonna be miserable forever.
        
            ** [Continue]
                -> Convo_2

-> Convo_2






