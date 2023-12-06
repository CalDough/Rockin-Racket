INCLUDE ../globals.ink

-> Testing

=== Testing ===

{
    - Has_Haley_Gift == true:
    
        Oh, is this for me? Thank you so much!
        ~ Given_Haley_Gift = true

    
    - else:
    
        What are you doing?
}

* [...]
    -> Again

=== Again ===
{
    - Given_Haley_Gift == true:
        
        Please stop, I don't want any more gifts right now
        
}

-> END