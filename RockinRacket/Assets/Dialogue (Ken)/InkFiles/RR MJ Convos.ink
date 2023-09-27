
//Naming - ex. H_H1_C1 (first letter (the H in this case) stands for the character (Haley in this case), the H1 stands for which hub it is in (Hub 1 in this case), and C1 is the dialogue choice)

=== Ending === ///This is just a place for the conversations to route back to so there are no errors
This is a placeholder
    * [...]
        -> END

=== H1 ===
= MJ
MJ: "What do you need? I'm busy."
    * "Sorry to interrupt[..."], I just wanted to see what you got going on."
        -> M_H1_C1
    * "How's your guitar[?"]? Is it doing well?"
        -> M_H1_C2
    * [End convo]
        -> Ending

= M_H1_C1
MJ: "I'm practicing some songs."
    * "Practicing on a Sunday[?"]? Shouldn't you be resting?"
        MJ: "Rest is for the weak. And I'm not weak."
            -> MJ
    * "How about taking[..."] a break?"
        MJ: "I'll take a break when I'm done. Can I get back to it now?"
            -> MJ

= M_H1_C2
"I could use one. You buying?"
    * "I got you.["] Give me a bit and you'll have it."
        "Thanks."
            -> MJ
    * "Maybe later.["] I need to rack up some more cash before I buy it."
        "I won't get my hopes up too much then."
            -> MJ







=== H2 ===
= MJ
MJ: "Need something?"
    * "Actually, yeah. Could you[..."] lower the volume a bit?"
        -> M_H2_C1
    * "Is your guitar[..."] holding up properly?"
        -> M_H2_C2
    * [End convo]
        -> Ending

= M_H2_C1
MJ: "Is it bothering you?"
    * "It is. I don't mind[..."] you practicing, but some of us are trying to get some sleep. So can you turn it down now?"
        MJ: "Fine. But next time just wait until I'm done before you go to bed."
            -> MJ
    * "Well considering you're in the garage[..."], making the sounds echo and become louder, the yes. It is bothering me."
        MJ: "Noted. I'll turn it down then."
            -> MJ

= M_H2_C2
"I could use one. You buying?"
    * "I got you.["] Give me a bit and you'll have it."
        "Thanks."
            -> MJ
    * "Maybe later.["] I need to rack up some more cash before I buy it."
        "I won't get my hopes up too much then."
            -> MJ





=== H3 ===
= MJ
MJ: "Look who it is. The betrayer."
    * "How many times do I[..."] need to apologize and explain myself to you guys?"
        -> M_H3_C1
    * [End convo]
        -> Ending

= M_H3_C1
MJ: "At least once more for me. How could you do something like that to us? To me?"
    * "I was originally hoping that[..."] the experience would help me get into college. But ever since the gig at the barn, I loved playing with you guys and college seemed less desirable."
        MJ: "You could have told us sooner about it. I'm sure we would have understood. But no, instead we had to figure it out ourselves after we grew attached to having you around helping us."
            -> MJ
    * "I wanted to tell you guys[..."], but I just got so caught up in working. It made me forget all about the application until it fell out of my pocket. I swear I didn't mean to hurt you guys."
        MJ: "Whether you meant to or not, it doesn't change the fact that you did. You broke our trust in you. That's something that's hard to fix, you know."
            -> MJ







=== H4 ===
= MJ
MJ: "You're one stubborn brother and stage manager. I respect that about you. I guess what I'm trying to say is that I want to work with you again."
    * I would as well[..."] MJ, so get in here!"
        MJ: "No more ulterior motives though. Deal?"
        "Consider it done. Where do I sign?"
            -> Ending
    * Thanks... I guess?["] That was a strange start to that statement, but I'll take it."
        MJ: "We're still missing Haley, huh? I'm sure she'll come around. It just hit her pride pretty hard."
        "I really hope she comes back around. Until then, let's just play some music."
            -> Ending






=== Convo_2 ===
= MJ
/// Make more MJ like
MJ: "What do you need? I'm kinda busy shopping online."
    * "Shopping?["] What for?"
        -> M_Convo2_C1
    * "Let me know when you're done["] then. I was gonna ask you something."
        -> M_Convo2_C2
        
= M_Convo2_C1
MJ: "If you must know, for a new figurine for Kurt. He helped me out last week so I figured I could return the favor by doing this."
    * "That's so nice of you["]. Are you sure you're the MJ I know?"
        MJ: "Ha. Ha. laugh it up. You're just jealous that I'm not buying you something. I'm right, aren't I?"
            ** [Continue]
                -> Convo_2
    * "He's gonna love it["]. It's gonna be like last Christmas all over again for him."
        MJ: "I know. He's gonna go crazy with joy. I'll keep a fire extinguisher ready to go though. Just in case he gets too crazy."
            ** [Continue]
                -> Convo_2

= M_Convo2_C2
MJ: "Well you've already interrupted me, so just ask now."
    * "If you say so["]. Can you help me with dinner tomorrow night? I'm gonna be trying out a new recipe and could use another set of paws for help."
        MJ: "I guess so. The others are horrible cooks anyways so they wouldn't be much help."
            ** [Continue]
                -> Convo_2
    * "Okay then["]. Do you have any of your old piano practice sheets from when you wanted to play that? I wanna try and learn to play myself."
        MJ: "Wait, really? You finally wanna learn an instrument? I have them in a box under my bed. Here, take them. There's an old keyboard in the garage closet too. I'm not as familiar with it now, but let me know if you want some assistance!"
            ** [Continue]
                -> Convo_2




