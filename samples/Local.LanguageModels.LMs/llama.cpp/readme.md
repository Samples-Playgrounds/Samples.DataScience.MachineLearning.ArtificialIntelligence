

*   https://github.com/SciSharp/LLamaSharp

    *   PInvoke (native) bindings

        *   https://github.com/ggml-org/llama.cpp

        *   https://github.com/SciSharp/LLamaSharp/tree/master/LLama/Native


*   https://www.reddit.com/r/LocalLLaMA/comments/1j417qh/llamacpp_is_all_you_need/

```shell
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

```shell
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

```shell
llama-server \
    -hf ggml-org/gpt-oss-20b-GGUF  \
    --ctx-size 0 \
    --jinja \
    -ub 2048 \
    -b 2048 \
    -ngl 99 \
    -fa auto
```



