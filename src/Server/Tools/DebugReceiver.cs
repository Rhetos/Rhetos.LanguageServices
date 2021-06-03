﻿/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using OmniSharp.Extensions.JsonRpc.Server;
using OmniSharp.Extensions.LanguageServer.Server;

namespace Rhetos.LanguageServices.Server.Tools
{
    // TODO: remove completely if not needed
    /*
    public class DebugReceiver : ILspReciever
    {
        private readonly Logger _logger;
        private readonly LspReciever _default;

        public DebugReceiver()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _default = new LspReciever();
        }

        public (IEnumerable<Renor> results, bool hasResponse) GetRequests(JToken container)
        {
            _logger.Info(container.ToString(Formatting.Indented));
            return _default.GetRequests(container);
        }

        public void Initialized()
        {
            _default.Initialized();
        }

        public bool IsValid(JToken container)
        {
            return _default.IsValid(container);
        }
    }
    */
}
