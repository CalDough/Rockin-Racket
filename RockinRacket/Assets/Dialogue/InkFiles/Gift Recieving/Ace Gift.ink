INCLUDE ../globals.ink

-> Has_Gift


=== Has_Gift ===

{

    - Has_Ace_Gift == true:
    
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        YOOO, for me? You shouldn't have my guy. I'm just yankin' your tail, this is the best thing I've gotten since my drums for sure! Imma cherish it for the rest of my life, I promise you that.
        
    - else:
    
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Wait, are we doing improv right now? There's an invisible plate of spaghetti in your hands, isn't there? No? Dang, I would've loved to act that one out cause I'm starving right now.
        
}

-> END

=== Given_Gift ===

{

    - Given_Ace_Gift == true:
    
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        My man, I appreciate the gifts, but get me some food instead sometimes. I can't take any more of these inedible objects right now.
        
}

-> END