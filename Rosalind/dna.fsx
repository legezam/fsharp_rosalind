#load "StringHelpers.fsx"
#load "IO.fsx"

let exercise = "dna"

open System
open System.Linq
open IO
let result = IO.ReadInput exercise
                |> Seq.filter (fun char -> char <> '\n')
                |> Seq.groupBy (fun item -> item)
                |> Seq.sortBy (fun (key, value) -> key)
                |> Seq.map (fun (_, value) -> value.Count())
                |> (fun items -> System.String.Join(" ", items))

IO.WriteOutput exercise result
