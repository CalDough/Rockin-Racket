INCLUDE ../globals.ink

-> Talking

=== Talking ===
#speaker: Jay #portrait: Jay_neutral
Of course I am! What's on your mind?
    -> Options




= Options

    * [About my siblings...]
        #speaker: Harvey #portrait: Harvey_neutral
        How do you deal with 15 year olds?
            -> Siblings


    * [You should come watch us.]
        #speaker: Harvey #portrait: Harvey_neutral
        You should swing by and watch our show sometime. We'd love to have a fellow musician be there.
            -> Invitation



    * [Nevermind.]
        #speaker: Harvey #portrait: Harvey_neutral
        Actually I don't have time to talk right now.

        #speaker: Jay #portrait: Jay_neutral
        No problem.
            -> END


= Siblings
#speaker: Jay #portrait: Jay_neutral
What do you mean?

    * [I'm worried...]
        #speaker: Harvey #portrait: Harvey_neutral
        I'm just worried about working with them. We don't exactly have the best relationship with each other.

            #speaker: Jay #portrait: Jay_neutral
            Just give it some time and that'll change. Don't stress too much about it right now.
                    -> Options

= Invitation
#speaker: Jay #portrait: Jay_neutral
Hmmm. My girlfriend has been wanting to go to more concerts...

    * [Perfect!]
        #speaker: Harvey #portrait: Harvey_neutral
        Don't want to disappoint her then. We have another one coming up soon so you can come to that one!

            #speaker: Jay #portrait: Jay_neutral
            Deal. We could do with a new band in our list anyways.

                -> Options


    * [Come see us!]
        #speaker: Harvey #portrait: Harvey_neutral
        You should definetly come see us play then. My siblings are super talented, I swear. We would love to have additional fans there to watch it.

            #speaker: Jay #portrait: Jay_neutral
            We'll see. I need to talk to her about it first.
                -> Options





-> END
