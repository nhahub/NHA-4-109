#!/bin/sh
cat > /app/jwtsettings.json <<EOF
{ "Jwt": { "AccessSecret": "$JWT_ACCESS_SECRET", "RefreshSecret": "$JWT_REFRESH_SECRET", "Issuer": "$JWT_ISSUER", "Audience": "$JWT_AUDIENCE", "AccessTokenMinutes": 20, "RefreshTokenDays": 7 } }
EOF
cat > /app/corssettings.json <<EOF
{ "Cors": { "ReactAppOrigin": "$CORS_REACT_APP_ORIGIN" } }
EOF
exec dotnet WebApplication1.dll
