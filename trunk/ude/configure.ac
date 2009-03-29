AC_PREREQ([2.54])
AC_INIT([Ude], [0.1])
AM_INIT_AUTOMAKE([foreign])
dnl AC_SUBST([ACLOCAL_AMFLAGS], ["-I m4 \${ACLOCAL_FLAGS}"])

dnl pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

SHAMROCK_EXPAND_LIBDIR
SHAMROCK_EXPAND_BINDIR
SHAMROCK_EXPAND_DATADIR

AC_PROG_INSTALL

AC_PATH_PROG(GMCS, gmcs, no)
if test "x$GMCS" = "xno"; then
    AC_MSG_ERROR([gmcs Not found])
fi

AC_ARG_ENABLE(debug_any_cpu,
	AC_HELP_STRING([--enable-debug_any_cpu],
		[Use 'DEBUG_ANY_CPU' Configuration [default=NO]]),
		enable_debug_any_cpu=yes, enable_debug_any_cpu=no)
AM_CONDITIONAL(ENABLE_DEBUG_ANY_CPU, test x$enable_debug_any_cpu = xyes)
if test "x$enable_debug_any_cpu" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release_any_cpu,
	AC_HELP_STRING([--enable-release_any_cpu],
		[Use 'RELEASE_ANY_CPU' Configuration [default=YES]]),
		enable_release_any_cpu=yes, enable_release_any_cpu=no)
AM_CONDITIONAL(ENABLE_RELEASE_ANY_CPU, test x$enable_release_any_cpu = xyes)
if test "x$enable_release_any_cpu" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
if test -z "$CONFIG_REQUESTED" ; then
	AM_CONDITIONAL(ENABLE_RELEASE_ANY_CPU, true)
	enable_release_any_cpu=yes
fi

dnl package checks, common for all configs
dnl PKG_CHECK_MODULES([MONO_NUNIT], [mono-nunit])
AC_ARG_ENABLE(tests,
    AC_HELP_STRING([--enable-tests],
        [build unit tests [default=yes]]),
        enable_tests=${enableval}, enable_tests=no)

AM_CONDITIONAL(ENABLE_TESTS, test x$enable_tests = xyes)

if test "x$enable_tests" = "xyes"; then
    PKG_CHECK_MODULES([MONO_NUNIT], [mono-nunit])
fi

AC_SUBST(MONO_NUNIT_LIBS)
AM_CONDITIONAL(ENABLE_TESTS, test x$enable_tests = xyes)

AC_CONFIG_FILES([
Makefile
m4/Makefile
src/Example/Makefile
src/Example/udetect
src/Library/ude.pc
src/Library/Makefile
src/Tests/Makefile
])

AC_OUTPUT

echo "
${PACKAGE}-${VERSION}

    Configuration summary
        * Install Prefix: ${prefix}
        * Datadir: ${expanded_datadir}
        * Libdir: ${expanded_libdir}
        * C# compiler: ${GMCS}
        * Unit tests: ${enable_tests}
"

