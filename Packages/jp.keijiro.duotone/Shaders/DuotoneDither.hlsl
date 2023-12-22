#if defined(_DITHERTYPE_BAYER2X2)

float DuotoneBayerArray[] =
{
    0.000000, 0.664062,
    0.996094, 0.332031,
};

static float DuotoneDither(uint2 psp)
{
    return DuotoneBayerArray[(psp.x & 1) + (psp.y & 1) * 2];
}

#elif defined(_DITHERTYPE_BAYER3X3)

float DuotoneBayerArray[] =
{
    0.000000, 0.871094, 0.371094,
    0.746094, 0.621094, 0.246094,
    0.496094, 0.121094, 0.996094,
};

static float DuotoneDither(uint2 psp)
{
    return DuotoneBayerArray[(psp.x % 3) + (psp.y % 3) * 3];
}

#elif defined(_DITHERTYPE_BAYER4X4)

float DuotoneBayerArray[] =
{
    0.000000, 0.531250, 0.132812, 0.664062,
    0.796875, 0.265625, 0.929688, 0.398438,
    0.199219, 0.730469, 0.066406, 0.597656,
    0.996094, 0.464844, 0.863281, 0.332031,
};

static float DuotoneDither(uint2 psp)
{
    return DuotoneBayerArray[(psp.x & 3) + (psp.y & 3) * 4];
}

#else

float DuotoneBayerArray[] =
{
    0.000000, 0.757812, 0.187500, 0.945312, 0.046875, 0.804688, 0.234375, 0.996094,
    0.503906, 0.250000, 0.695312, 0.441406, 0.550781, 0.296875, 0.742188, 0.488281,
    0.125000, 0.882812, 0.062500, 0.820312, 0.171875, 0.929688, 0.109375, 0.867188,
    0.628906, 0.378906, 0.566406, 0.312500, 0.679688, 0.425781, 0.613281, 0.363281,
    0.031250, 0.789062, 0.218750, 0.976562, 0.015625, 0.773438, 0.203125, 0.960938,
    0.535156, 0.281250, 0.726562, 0.472656, 0.519531, 0.265625, 0.710938, 0.457031,
    0.156250, 0.914062, 0.093750, 0.851562, 0.140625, 0.898438, 0.078125, 0.835938,
    0.664062, 0.410156, 0.597656, 0.347656, 0.644531, 0.394531, 0.582031, 0.332031
};

static float DuotoneDither(uint2 psp)
{
    return DuotoneBayerArray[(psp.x & 7) + (psp.y & 7) * 8];
}

#endif
