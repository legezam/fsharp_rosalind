open System
open System.IO

Environment.CurrentDirectory <- "/Users/legezam/Projects/Rosalind/Rosalind"

module IO =
    let inputFormat = "input/rosalind_%s.txt"
    let outputFormat = "output/rosalind_%s_result.txt"
    let ReadInputLines (exerciseName : string) =
        File.ReadAllLines(sprintf (Printf.StringFormat<_>(inputFormat)) exerciseName)

    let ReadInput (exerciseName : string) =
        File.ReadAllText(sprintf (Printf.StringFormat<_>(inputFormat)) exerciseName)

    let WriteOutputLines (exerciseName : string) (content : string seq) =
        File.WriteAllLines((sprintf (Printf.StringFormat<_>(outputFormat)) exerciseName), content)
    
    let WriteOutput (exerciseName : string) (content : string) =
        File.WriteAllText((sprintf (Printf.StringFormat<_>(outputFormat)) exerciseName), content)