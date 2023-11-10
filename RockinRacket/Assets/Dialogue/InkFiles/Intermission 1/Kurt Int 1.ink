INCLUDE ../globals.ink

-> Convo


=== Convo ===
#speaker: Kurt #portrait: kurt_speaking_normal
#portrait: harvey_chill_normal
Oh! H-hey Harvey. Is s-something wrong?

    * [Not at all]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Nope. Or at least not that I know of right now. I just wanted to talk.
        -> C1


    * [Are you good?]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Are you okay? You look like you're about to fall apart.
        -> C2



= C1
#speaker: Kurt #portrait: kurt_speaking_normal
#portrait: harvey_chill_normal
O-okay. How's your s-side of the c-conert going?

    * [Pretty good]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    I feel like it's going good so far. Nothing too troublesome has popped up.
    
    #speaker: Kurt #portrait: kurt_speaking_normal
    #portrait: harvey_chill_normal
    That's g-good. I was worried y-you'd have some d-d-difficulties, but I g-gues not. K-keep up the good w-work!
        -> END
    
    * [Not the best]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    I can't tell. It's all just so much and I feel like I'm messing up so many things.
    
    #speaker: Kurt #portrait: kurt_speaking_normal
    #portrait: harvey_chill_normal
    It's hard s-starting out. Everything is n-new and it's t-tiring, but it's w-worth it in the end. J-just keep at it b-bro!
        -> END



= C2
#speaker: Kurt #portrait: kurt_speaking_normal
#portrait: harvey_chill_normal
Y-yeah, I'm okay. There's j-just more people than I t-thought there would b-be.

    * [Now that you mention it]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Well now that you pointed it out. There does seem to be several people here. I thought you guys weren't that popular?
    
    #speaker: Kurt #portrait: kurt_speaking_normal
    #portrait: harvey_chill_normal
    M-maybe word has f-finally spread a-about us? That's great! But also k-kinda nerve-wracking. You b-being here will help l-later on though.
        -> END
    
    
    * [You're scared of them?]
    #speaker: Kurt #portrait: kurt_chill_normal
    #speaker: Harvey #portrait: harvey_speaking_normal
    Are you scared of the audience? Why are you playing for them then?
    
    #speaker: Kurt #portrait: kurt_speaking_normal
    #portrait: harvey_chill_normal
    I'm n-not scared, just a b-bit anxious. I'll be f-fine though. Thanks for w-worrying about m-me.
        -> END


-> END