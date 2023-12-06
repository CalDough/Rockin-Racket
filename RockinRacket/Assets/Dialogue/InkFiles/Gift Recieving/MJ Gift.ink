INCLUDE ../globals.ink

-> Has_Gift


=== Has_Gift ===

{

    - Has_MJ_Gift == true:
    
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        Okay, now this is UBER rad. I don't know what little voice told you to get me this, but listen to it more often, you big stinker. I haven't gotten something this cool since our 11th birthday party.
        
    - else:
    
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        Can I help you? Are you having a stroke or something? Keep your empty paws away from me. I'm not afraid to scratch your face. I'll do it, I really will.
        
}

-> END

=== Given_Gift ===

{

    - Given_MJ_Gift == true:
    
        #speaker: MJ #portrait: harvey_chill_normal #portrait: mj_speaking_normal
        Oh no. Another one? Please give it a rest. I don't need anymore of your gifts right now. The first one was nice, but no you just seem desperate. 
        
}

-> END