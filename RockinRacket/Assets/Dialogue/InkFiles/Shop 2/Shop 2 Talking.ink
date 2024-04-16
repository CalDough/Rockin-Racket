INCLUDE ../globals.ink

-> Talking

=== Talking ===

#speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
Sure! What's up?
    -> Options

    

= Options

    * [What a gig.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        We just had the craziest gig so far. We had it in a barn, which I thought was gonna be nicer, but it was littered with hay and other random stuff.
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Despite that, it was an amazing place.
            -> C1
    
    
    * [I'm having fun.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        I know last time we talked I was unsure of how it was gonna go with the band. I'm happy to say now that I worried about it for nothing. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Everything's going great with them!
            -> C2
    
    
    
    * [Nevermind.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        Actually, I don't really feel like talking right now. Sorry.
        
        
        #speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
        No need to apologize, we all have those moments.
        
        
            -> END


= C1

#speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
Sounds like it would be a nightmare to play in. With all the hay getting in your fur, and the smell of a barn, you know?

    * [It wasn't bad.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Well once you get used to it, it isn't actually that bad. With all the people there, it kinda masked the barn smell too. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        Although I do think I still have some hay stuck in my fur, so maybe that's the biggest issue.
            
            
        #speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
            Yeah, you're probably gonna be brushing that out for a few more days at least.
                -> Options
    
    * [Sometimes it was.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        Yeah, it got bad a few times. Mainly at start and end because we were the first there and the last to leave, so we dealt with it the most.
            
            
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        That's band life for you, especially as a smaller one. You need to set up and tear down your own gear. 
        
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        Once you're more popular, you might be able to hire people to do that for you.
            -> Options

= C2

#speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
That's great to hear kiddo! What do you think did it?

    * [Time.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Honestly, I think it was just giving them a bit of time to adjust to me being there with them. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        It's not like they were totally against me working with them, or else they wouldn't have wanted me to help in the first place.
            
            
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        See, I told you it would work out if you had a bit of patience.
            
            -> Options
    
    
    * [My skills.]
        
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: jay_chill_normal
        Sounds kinda egotistical to say, but I think it was thanks to how well I've been doing and how fast I can adapt to changes? 
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        They've really been liking that from me, so maybe that's all it took.
        
            
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        If they liked what you were doing, then that's for sure a huge plus.
            -> Options


-> END


