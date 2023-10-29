INCLUDE ../globals.ink

-> H2

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