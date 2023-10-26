VAR bought_item = false

INCLUDE ../globals.ink

-> Start

=== Start ===
~ bought_item = false
Hey there kiddo! What can I do for you?

    * [A bunch]
        
        A bunch. I just started helping my younger siblings with their band.
            -> talking
    

    * [What equipment do you have?]
        -> Shopping
        
    * [Nevermind]
    
        Sorry, I changed my mind. I'm gonna go.
        
        All good! Have a great day kiddo!
        
            -> END





= talking

Ooo, that sounds fun. How are you liking it so far?

    * [It's pretty fun]
    
        It's actually pretty fun. It's a bit hectic, but still interesting in my book.
            -> Positive

    
    * [Don't know yet]
    
        I'm still figuring that one out


= Positive


= Negative


= Shopping
Well here, take this. Take as long as a look that you want.





-> END