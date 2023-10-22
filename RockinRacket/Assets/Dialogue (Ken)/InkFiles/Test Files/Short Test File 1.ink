INCLUDE ../globals.ink
This is a very short dialogue test file. #speaker: Test #portrait: Harvey_neutral
-> Question

=== Question ===
= QuestionStart
If you select "fire" you will get passed the question.
 * fire
    Ay, you can read!
    -> QuestionEnd
 * water
    Did you read?
    -> QuestionStart
 * earth
    Did you read?
    ->QuestionStart
=QuestionEnd
Alright we're done! Goodbye now
    -> END
