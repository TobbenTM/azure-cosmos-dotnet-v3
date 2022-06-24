﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class OpenTelemetryTests
    {
        private static ClientDiagnosticListener testListener;

        public static void ClassInitialize(TestContext context)
        {
            OpenTelemetryTests.testListener = new ClientDiagnosticListener("Azure.Cosmos");
        }

        public static void FinalCleanup()
        {
            OpenTelemetryTests.testListener.Dispose();
        }
    }
}