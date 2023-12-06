INCLUDE ../globals.ink

-> Has_Gift


=== Has_Gift ===

{

    - Has_Kurt_Gift == true:
    
        #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal
        For me? R-really! This is gonna go great with the rest of my c-collection. I'll need to make room of course, but I c-can squeeze it in between the actions figures somewhere. 
        
    - else:
    
        #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal
        What's going on? Is this a p-prank or something? D-did MJ put you up to this? Please stop. You and your empty paws are s-scaring me.
        
}

-> END

=== Given_Gift ===

{

    - Given_Kurt_Gift == true:
    
        #speaker: Kurt #portrait: harvey_chill_normal #portrait: kurt_speaking_normal
        Oh, um... Another one? I think I c-can fit it on my shelf somewhere. Or maybe in the b-bookcase? Please no more though. I'm running out of room.
        
}

-> END