﻿// ----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ----------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Performance.Tests.Benchmarks
{
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;

    public enum ScenarioType
    {
        Stream = 0,
        OfT = 1,
        OfTCustom = 2,
        OfTWithDiagnosticsToString = 3,
        OfTWithClientTelemetryEnabled = 4
    }

    [Config(typeof(SdkBenchmarkConfiguration))]
    public class MockedItemBenchmark : IItemBenchmark
    {
        public static readonly IItemBenchmark[] IterParameters = new IItemBenchmark[]
            {
                new MockedItemStreamBenchmark(),
                new MockedItemOfTBenchmark() { BenchmarkHelper = new MockedItemBenchmarkHelper() },
                new MockedItemOfTBenchmark() { 
                    BenchmarkHelper = new MockedItemBenchmarkHelper(
                        useCustomSerializer: true) },
                new MockedItemOfTBenchmark() { 
                    BenchmarkHelper = new MockedItemBenchmarkHelper(
                        useCustomSerializer: false, 
                        includeDiagnosticsToString: true) },
                new MockedItemOfTBenchmark() { 
                    BenchmarkHelper = new MockedItemBenchmarkHelper(
                        useCustomSerializer: false, 
                        includeDiagnosticsToString: false,
                        isClientTelemetryEnabled: true) }
            };

        [Params(
            ScenarioType.Stream, 
            ScenarioType.OfT, 
            ScenarioType.OfTWithDiagnosticsToString, 
            ScenarioType.OfTCustom,
            ScenarioType.OfTWithClientTelemetryEnabled)]
        public ScenarioType Type
        {
            get;
            set;
        }

        private IItemBenchmark CurrentBenchmark => MockedItemBenchmark.IterParameters[(int)this.Type];

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task CreateItem()
        {
            await this.CurrentBenchmark.CreateItem();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task DeleteItemExists()
        {
            await this.CurrentBenchmark.DeleteItemExists();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task DeleteItemNotExists()
        {
            await this.CurrentBenchmark.DeleteItemNotExists();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task ReadFeed()
        {
            await this.CurrentBenchmark.ReadFeed();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task ReadItemExists()
        {
            await this.CurrentBenchmark.ReadItemExists();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task ReadItemNotExists()
        {
            await this.CurrentBenchmark.ReadItemNotExists();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task UpdateItem()
        {
            await this.CurrentBenchmark.UpdateItem();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task UpsertItem()
        {
            await this.CurrentBenchmark.UpsertItem();
        }

        [Benchmark]
        [GcForce, GcServer]
        [BenchmarkCategory("GateBenchmark")]
        public async Task QuerySinglePage()
        {
            await this.CurrentBenchmark.QuerySinglePage();
        }
    }
}