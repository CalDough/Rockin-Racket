INCLUDE ../globals.ink

-> Talking

=== Talking ===
#speaker: Jay #portrait: jay_speaking_normal
#portrait: harvey_chill_normal
Of course I am! What's on your mind?
    -> Options




= Options

    * [About my siblings...]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        How do you deal with 15 year olds?
            -> Siblings


    * [You should come watch us.]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        You should swing by and watch our show sometime. We'd love to have a fellow musician be there.
            -> Invitation



    * [Nevermind.]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Actually I don't have time to talk right now.

        #portrait: jay_speaking_normal
        #speaker: Jay #portrait: harvey_chill_normal
        No problem.
            -> END


= Siblings
#speaker: Jay #portrait: jay_speaking_normal
#portrait: harvey_chill_normal
What do you mean?

    * [I'm worried...]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        I'm just worried about working with them. We don't exactly have the best relationship with each other.

            #speaker: Jay #portrait: jay_speaking_normal
            #portrait: harvey_chill_normal
            Just give it some time and that'll change. Don't stress too much about it right now.
                    -> Options

= Invitation
#speaker: Jay #portrait: jay_speaking_normal
#portrait: harvey_chill_normal
Hmmm. My girlfriend has been wanting to go to more concerts...

    * [Perfect!]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        Don't want to disappoint her then. We have another one coming up soon so you can come to that one!

            #speaker: Jay #portrait: jay_speaking_normal
            #portrait: harvey_chill_normal
            Deal. We could do with a new band in our list anyways.

                -> Options


    * [Come see us!]
        #portrait: jay_chill_normal
        #speaker: Harvey #portrait: harvey_speaking_normal
        You should definitely come see us play then. My siblings are super talented, I swear. We would love to have additional fans there to watch it.

            #speaker: Jay #portrait: jay_speaking_normal
            #portrait: harvey_chill_normal
            We'll see. I need to talk to her about it first.
                -> Options





-> END
