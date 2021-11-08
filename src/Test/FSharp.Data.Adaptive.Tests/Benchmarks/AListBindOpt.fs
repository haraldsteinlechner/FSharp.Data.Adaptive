namespace Benchmarks

open System
open FSharp.Data.Adaptive
open FSharp.Data.Traceable
open BenchmarkDotNet.Attributes
open System.Runtime.CompilerServices
open BenchmarkDotNet.Configs


[<PlainExporter; MemoryDiagnoser>]
[<GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)>]
type BindOptBenchmark() =

    let inner = cval 1

    let l = IndexList.ofList [1..1000]
    let a = clist l
    let b = clist l

    let blu = inner |> AList.bind (fun v -> if v % 2 = 0 then a |> AList.map id else b |> AList.map id)
    let reader = blu.GetReader()

   
    //[<Params(1, 10, 50, 100, 1000, 10000); DefaultValue>]
    //val mutable public Count : int


    [<Benchmark;>]
    member x.AListBindOptimization() =
        transact (fun _ -> inner.Value <- inner.Value + 1 )
        reader.Update(AdaptiveToken.Top)

        
