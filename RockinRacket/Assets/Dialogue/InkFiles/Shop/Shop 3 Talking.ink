INCLUDE ../globals.ink

-> Talking

=== Talking ===

#speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
I am just for you. What's up kiddo?
    -> Options

    


= Options

    * [We won a tournament!]
        
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: jay_chill_normal
        Did you hear? We won Battle of the Bands! 
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Well, we still have the finals to get through, but we made it all the way there!
            -> C1
    
    
    * [How do we sound?]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        I saw you at one of our gigs! Well? Did you like what you heard?
            -> C2
    
    
    
    * [I should leave.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        I really appreciate you talking with me Jay. See ya later!
        
        
        #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
        It's all good. Have a good day kiddo!
            -> END


= C1

#speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
No way! My girlfriend said you guys were competing, but I didn't know you made it that far. 

#speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
Congrats to you guys!

    * [Thanks!]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Thank you! I'll let the band know you said that.
        
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: jay_chill_normal
        And if you're free, we'd all love it if you were there to support us.
        
            #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
            I'll see what I can do. Maybe I'll surprise my girlfriend with the tickets.
        
        -> Options
        


= C2

#speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
You guys were epic. I didn't know those little siblings of yours had it in them.

    * [They're feisty.]
        
        #speaker: Harvey #portrait: harvey_speaking_annoyed #portrait: jay_chill_normal
        Oh you should see them off the stage too. They can really be a paw-full to deal with.
            
            
            #speaker: Jay #portrait: harvey_chill_normal #portrait: jay_speaking_normal
            I bet. Teens are usually like that.
            
            
            #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
            But at least yours are cool too.
            
                -> Options
    
    
    * [They practice a lot.]
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        They practice like crazy, so I'll let them know it's paying off. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: jay_chill_normal
        Sometimes they wake the neighbors and we get complaints.
        
            
            #speaker: Jay #portrait: harvey_chill_normal-alt #portrait: jay_speaking_normal
            Yikes. I bet it's worth it though in the long run for them. A little bit of haters aren't stopping them!
                -> Options





-> END