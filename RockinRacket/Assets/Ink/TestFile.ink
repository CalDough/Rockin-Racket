VAR SelectedAttribute = ""

-> Main
=== Main ===

Do you think I Should Be learn a different instrument for the Band? # Band Member

 + Yes, Keyboard # You
    ~ SelectedAttribute = "Keyboard"
    -> Chosen
 + Yes, Strings # You
    ~ SelectedAttribute = "Strings"
    -> Chosen
 + Yes, Percussion # You
    ~ SelectedAttribute = "Percussion"
    -> Chosen
 + Yes, Woodwings # You
    ~ SelectedAttribute = "Woodwinds"
    -> Chosen
 + Yes, Vocals # You
    ~ SelectedAttribute = "Vocals"
    -> Chosen
 + No, Don't Change # You
    ~ SelectedAttribute = ""
    -> Chosen
  
  
=== Chosen ===
{SelectedAttribute == "":
    You didn't change their instrument preference. # You
  - else:
    The instrument choice was {SelectedAttribute}! # You
}


-> END
