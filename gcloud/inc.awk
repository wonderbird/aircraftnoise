#!/usr/bin/awk -f
#
# Bump file version
#
# Adopted from https://stackoverflow.com/questions/8653126/how-to-increment-version-number-in-a-shell-script

# Demo different increments
#BEGIN{
#    v[1] = "1.2.3.4"
#    v[2] = "1.2.3.44"
#    v[3] = "1.2.3.99"
#    v[4] = "1.2.3"
#    v[5] = "9"
#    v[6] = "9.9.9.9"
#    v[7] = "99.99.99.99"
#    v[8] = "99.0.99.99"
#    v[9] = ""
#
#    for(i in v)
#        printf("#%d: %s => %s\n", i, v[i], inc(v[i])) | "sort | column -t"
#}

{
  printf("%s\n", inc($0))
}

function inc(s,    a, len1, len2, len3, head, tail)
{
    split(s, a, ".")

    len1 = length(a)
    if(len1==0)
        return -1
    else if(len1==1)
        return s+1

    len2 = length(a[len1])
    len3 = length(a[len1]+1)

    head = join(a, 1, len1-1)
    tail = sprintf("%0*d", len2, (a[len1]+1)%(10^len2))

    if(len2==len3)
        return head "." tail
    else
        return inc(head) "." tail
}

function join(a, x, y,    s)
{
    for(i=x; i<y; i++)
        s = s a[i] "."
    return s a[y]
}
