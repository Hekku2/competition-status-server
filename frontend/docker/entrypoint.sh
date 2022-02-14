#!/bin/sh

set -e

# Configuration from environment variables. For example FRONTEND_apiUri -> apiUri.
if [ ! -f "$CONFIG_JSON_PATH" ]; then
    echo "Config file $CONFIG_JSON_PATH not found! Set in env variable CONFIG_JSON_PATH"
    exit 1
fi

temp_config="$(dirname $0)/inplacetemp.json"

while IFS='' read -r line || [ -n "$line" ]; do
    remainder="$line"
    name="${remainder%%=*}"; remainder="${remainder#*=}"
    value="${remainder%%=*}";

    case $name in
        "FRONTEND_"*)
            echo "$name is FRONTEND configuration, applying to config file."
            field_name="${name#*_}"
            jq ".${field_name} = \"$value\"" "$CONFIG_JSON_PATH" > "$temp_config"

            # jq doesn't support in place file edits
            mv -f "$temp_config" "$CONFIG_JSON_PATH"
            ;;
        *) ;;
    esac
done <<EOF
$(env)
EOF

nginx -g "daemon off;"
