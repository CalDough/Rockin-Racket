INCLUDE ../globals.ink

-> Has_Gift


=== Has_Gift ===

{

    - Has_Haley_Gift == true:
    
        #speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
        NO FLUFFING WAY! THIS IS FOR ME?! You have just earned yourself the best big bro award, or at least until it expires.
        
    - else:
    
        #speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
        Um... What are you doing? I don't have anything to give you if that's what you want.
        
}

-> END

=== Given_Gift ===

{

    - Given_Haley_Gift == true:
    
        #speaker: Haley #portrait: harvey_chill_normal #portrait: haley_speaking_normal
        I don't have the space for anymore gifts right now bro. Please just stop, its getting a bit weird.
        
}

-> END