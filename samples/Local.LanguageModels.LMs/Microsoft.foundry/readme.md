
```bash
llama-server \
    -hf ggml-org/gpt-oss-120b-GGUF  \
    --port 8080 \
    --ctx-size 0 \
    --jinja \
    -ub 2048 \
    -b 2048 \
    -ngl 99 \
    -fa auto
```

```bash
llama-server \
    -hf ggml-org/gpt-oss-120b-GGUF  \
    --port 9090 \
    --ctx-size 0 \
    --jinja \
    -ub 2048 \
    -b 2048 \
    -ngl 99 \
    -fa auto
```

```bash
llama-server \
    -hf ggml-org/gpt-oss-20b-GGUF  \
    --ctx-size 0 \
    --jinja \
    -ub 2048 \
    -b 2048 \
    -ngl 99 \
    -fa auto
```



