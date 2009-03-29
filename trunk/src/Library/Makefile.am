
EXTRA_DIST =  
BUILD_DIR=$(top_builddir)/bin
ASSEMBLY = $(BUILD_DIR)/Ude.dll
COMPILE_TARGET = library
LINUX_PKGCONFIG = $(UDE_PC) 
 
if ENABLE_DEBUG_ANY_CPU
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"
ASSEMBLY_MDB = $(ASSEMBLY).mdb
endif

if ENABLE_RELEASE_ANY_CPU
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:TRACE"
ASSEMBLY_MDB = 
endif

all: $(ASSEMBLY) $(LINUX_PKGCONFIG) 

FILES = \
	Ude.Core/CharDistributionAnalyser.cs \
	Ude.Core/CharsetProber.cs \
	Properties/AssemblyInfo.cs \
	Ude.Core/CodingStateMachine.cs \
	Ude.Core/EscCharsetProber.cs \
	Ude.Core/EscSM.cs \
	Ude.Core/HebrewProber.cs \
	Ude.Core/Latin1Prober.cs \
	Ude.Core/MBCSGroupProber.cs \
	Ude.Core/BitPackage.cs \
	Ude.Core/SBCharsetProber.cs \
	Ude.Core/SBCSGroupProber.cs \
	Ude.Core/UniversalDetector.cs \
	Ude.Core/UTF8Prober.cs \
	Ude.Core/MBCSSM.cs \
	Ude.Core/SequenceModel.cs \
	Ude.Core/GB18030Prober.cs \
	Ude.Core/SMModel.cs \
	Ude.Core/LangCyrillicModel.cs \
	Ude.Core/LangBulgarianModel.cs \
	Ude.Core/LangHebrewModel.cs \
	Ude.Core/LangGreekModel.cs \
	Ude.Core/LangHungarianModel.cs \
	Ude.Core/LangThaiModel.cs \
	Ude.Core/Big5Prober.cs \
	Ude.Core/EUCJPProber.cs \
	Ude.Core/EUCKRProber.cs \
	Ude.Core/EUCTWProber.cs \
	Ude.Core/JapaneseContextAnalyser.cs \
	Ude.Core/SJISProber.cs \
	Ude/ICharsetDetector.cs \
	Ude/CharsetDetector.cs \
	Ude/DetectionConfidence.cs \
	Ude.Core/Charsets.cs 

EXTRAS = ude.pc.in 
REFERENCES = System 
UDE_PC = $(BUILD_DIR)/ude.pc

CLEANFILES = $(LINUX_PKGCONFIG) $(ASSEMBLY) $(ASSEMBLY_MDB) $(UDE_PC)
MAINTAINERCLEANFILES = Makefile.in

include $(top_srcdir)/Makefile.include

$(eval $(call emit-deploy-wrapper,UDE_PC,ude.pc))

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) 
	mkdir -p $(dir $(ASSEMBLY))
	gmcs $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_references_ref)

