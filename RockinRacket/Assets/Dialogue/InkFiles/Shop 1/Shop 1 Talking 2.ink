INCLUDE ../globals.ink

-> Talking

=== Talking ===

#speaker: Jay #portrait: jay_speaking_normal #portrait: harvey_chill_normal-alt
Of course I am! What's on your mind?
    -> Options

    


= Options

    * [About my siblings.]
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: jay_chill_normal
        How do you deal with 15 year olds?
            -> Siblings
    
    
    * [You should come watch us.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        You should swing by and watch our show sometime. We'd love to have a fellow musician be there.
            -> Invitation
    
    
    
    * [I should go now.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        Thanks for talking with me, but I should really get going.
        
        
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        It's all good. Have a good day kiddo!
            -> END


= Siblings

#speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
What do you mean?

    * [I'm worried.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: jay_chill_normal
        I'm just worried about working with them. We haven't exactly had the closest relationship in the past.
            
            
            #speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
            Just because it was rocky in the past doesn't mean it's not alright now. Don't stress too much about it.
                    -> Options

= Invitation

#speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
Hmmm. My girlfriend has been wanting to go to more concerts...

    * [Perfect!]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Don't want to disappoint her then. We have another one coming up soon so you can come to that one!
            
            
            #speaker: Jay #portrait: harvey_chill_ambitious #portrait: jay_speaking_normal
            Deal. We could do with a new band in our list anyways.
            
                -> Options
    
    
    * [Come see us!]
        
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: jay_chill_normal
        You should definitely come see us play then. My siblings are super talented, I swear. We would love to have additional fans there to watch it.
        
            
            #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
            We'll see. I need to talk to her about it first.
                -> Options





-> END