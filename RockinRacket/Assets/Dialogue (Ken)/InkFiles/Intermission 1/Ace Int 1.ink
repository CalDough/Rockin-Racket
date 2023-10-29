INCLUDE ../globals.ink

-> Convo


=== Convo ===
#speaker: Ace #portrait: Ace_neutral
Yooo, Harv-man. What up?

    * [You're killing it]
    #speaker: Harvey #portrait: Harvey_neutral
    You are absolutely killing it over here.
        ->C1
    
    
    * [How can you do this?]
    #speaker: Harvey #portrait: Harvey_neutral
    How are you able to keep up with all the things you have to play? Sounds like a nightmare to me.
        ->C2


= C1
#speaker: Ace #portrait: Ace_neutral
You're too kind my guy. I'm just vibing with the others and playing from the heart.

    * [That's all it is?]
    #speaker: Harvey #portrait: Harvey_neutral
    Is that really all it is? "Playing from the heart?"
    
    #speaker: Ace #portrait: Ace_neutral
    Well that and a bunch of practice. As MJ says, "practice makes pizza." Or something like that I guess. Pizza's pretty good so it checks out.
        -> END
            
    * [But still...]
    #speaker: Harvey #portrait: Harvey_neutral
    Regardless of what it is, it's still pretty cool. Keep up the good work!
    
    #speaker: Ace #portrait: Ace_neutral
    You too man. I see you over there handling everything else. I see you, and I appreciate you.
        -> END

= C2
#speaker: Ace #portrait: Ace_neutral
Honestly man, sometimes I just zone out and let muscle memory take over. It's been working out so far.

    * [There's no way]
    #speaker: Harvey #portrait: Harvey_neutral
    How does that even work though? It sounds like it isn't real.
    
    #speaker: Ace #portrait: Ace_neutral
    I'm telling you man. I hit a zen state and everything just flows. Maybe you'll hit that kind of state eventually?
        -> END

    * [That can't be good]
    #speaker: Harvey #portrait: Harvey_neutral
    That sounds pretty risky to me though, zoning out in the middle of playing? Do the others care about it at all?
    
    #speaker: Ace #portrait: Ace_neutral
    Nah. We talked about it one night during practice and they all seemed cool with it. Or at least until I mess up, and then they might be mad at me for it. Just worry baout your stuff for now man, you're still learning after all.
        -> END


-> END