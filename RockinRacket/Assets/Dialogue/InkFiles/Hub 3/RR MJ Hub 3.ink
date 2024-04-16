INCLUDE ../globals.ink

-> H3

=== H3 ===
= MJ
#speaker: MJ #portrait: mj_speaking_normal #portrait: harvey_chill_intrigued
Gotta keep practicing. Gotta win. Gotta keep working. Gotta win.

    * [MJ?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_irritated
        Are you okay MJ? You don't, uh, look so good right now.
        
        -> M_H3_C1
    
    * [It's that important?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_normal
        Is winning this super important for you or something?
        
        -> M_H3_C2

= M_H3_C1
#speaker: MJ #portrait: mj_speaking_very-irritated #portrait: harvey_chill_normal
Oh, yeah. I think. I'm just stressed. Like SUPER stressed right now. I just want to win, like REAL bad.

    * [We got this!]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: mj_chill_irritated
        
        Don't worry too much about it MJ. We are gonna crush it out there!
        
        #speaker: MJ #portrait: mj_speaking_irritated #portrait: harvey_chill_nambitious
        Oh, I know we have a good chance at winning. That doesn't help those pesky bouts of stress though.
        
        -> END
    
    * [And if we don't?]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: mj_chill_offended
        
        Hate to say this, but have you thought about what you'll do if we lose? There's always that possibility ya know.
        
        #speaker: MJ #portrait: mj_speaking_offended #portrait: harvey_chill_normal
        DON'T SAY THAT! You're gonna fluffing jinx us if you stay stuff like that. I'd just rather not think about that possibility right now, okay?
        
        
        -> END






= M_H3_C2
#speaker: MJ #portrait: mj_speaking_normal-alt2 #portrait: harvey_chill_normal
DUH. This is like the biggest obstacle to stardom we've faced so far. Winning this can get us into the big leagues, so of course it's important!

    * [Right. Sorry.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_normal
        Yeah, of course. Sorry. I know it's a great opportunity and everything for us, but I didn't know how you felt about it on your own. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: mj_chill_offended
        Like it may be more important to you than it is to Ace. That kind of thing.
        
        #speaker: MJ #portrait: mj_speaking_disgusted #portrait: harvey_chill_normal-alt
        We both know he cares about other stuff more than this, so don't lump me in with him please. 
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_intrigued
        You could probably wave a candy bar in front of his face and he'd break his drum sticks in a heartbeat for it.
        
        -> END
    
    * [What about the others?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: mj_chill_normal
        Do you think the others feel the same way? Like, what if they aren't as hyped about this as you are?
        
        #speaker: MJ #portrait: mj_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        Trust me, they all know how important it is for us. Although Haley might be the only other one as dedicated and excited for this chance as me.
        
        -> END






