#load "StringHelpers.fsx"
#load "IO.fsx"

let exercise = "rna"

open System
open System.Linq
open IO

let result = IO.ReadInput exercise
                |> Seq.map (fun item -> if item = 'T' then 'U' else item)
                |> (fun chars -> String.Join("", chars))


IO.WriteOutput exercise result
