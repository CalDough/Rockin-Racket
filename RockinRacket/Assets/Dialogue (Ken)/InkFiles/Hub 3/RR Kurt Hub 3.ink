INCLUDE ../globals.ink

-> H3

=== H3 ===
= Kurt
#speaker: Kurt #portrait: Kurt_neutral
P-p-please don't tell me you're here t-t-to hurt me again?
    * [No, I'm not]
        #speaker: Harvey #portrait: Harvey_neutral
        No, I'm not Kurt. I wanted to say that I'm sorry for the pain I caused you. I know what I did was bad, but you have to believe me when I say that I changed.
        
        -> K_H3_C1
    * [End convo]
        -> END


= K_H3_C1
#speaker: Kurt #portrait: Kurt_neutral
A-a-apoligizing won't heal the p-p-pain. I'm more upset with m-m-myself for believing you when you offered t-t-to help us. You hate that k-kind of thing.
    * [This time was different]
        #speaker: Harvey #portrait: Harvey_neutral
        This time was different. I really started to love helping you guys. It gave me something to look forward to.
        
        #speaker: Kurt #portrait: Kurt_neutral
        Well n-n-now that's gone, so way to g-g-go.
        
            -> END
    * [Please don't beat yourself up...]
        #speaker: Harvey #portrait: Harvey_neutral
        Please don't beat yourself up about it, it's my fault. I didn't want to risk telling you guys so I thought if I ignored it then it would all be okay.
        
        #speaker: Kurt #portrait: Kurt_neutral
        You're m-m-mistake was keeping the ap-p-plication. You should have t-t-tossed it out.
        
            -> END